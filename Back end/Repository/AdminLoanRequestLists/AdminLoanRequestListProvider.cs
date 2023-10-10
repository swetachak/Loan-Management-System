using LMS.Models;
using Microsoft.EntityFrameworkCore;

namespace LMS.Data
{ 
    public class AdminLoanRequestListProvider : IAdminLoanRequestListProvider
    {
        private readonly Lms3Context _context;
        public AdminLoanRequestListProvider(Lms3Context context)
        {
            _context = context;
        }

        public async Task<List<LoanRequestDto>> GetPendingLoanRequestsAsync()
        {
            var waitingRequests = await _context.LoanRequests
                .Include(lr => lr.Employee)
                .Include(lr => lr.Item)
                .Where(lr => lr.Item.IssueStatus == "waiting")
                .ToListAsync();

            Console.WriteLine("Hello");
            if (waitingRequests == null || waitingRequests.Count == 0)
            {
                return new List<LoanRequestDto>(); 
            }
            var waitingRequestDtos = waitingRequests.Select(lr => new LoanRequestDto
            {
                EmployeeName = lr.Employee.EmployeeName,
                Designation = lr.Employee.Designation,
                Department = lr.Employee.Department,
                Gender = lr.Employee.Gender,
                ItemCategory = lr.Item.ItemCategory,
                ItemDescription = lr.Item.ItemDescription,
                ItemMake = lr.Item.ItemMake,
                ItemValuation = lr.Item.ItemValuation,
                RequestId = lr.RequestId.ToString(),
            }).ToList();

            return waitingRequestDtos;

        }
        public async Task<bool> ApproveLoanRequestAsync(string requestId)
        {
            Guid ConvRequestId;
            Guid.TryParse(requestId, out ConvRequestId);
            var loanRequest = await _context.LoanRequests
                                    .Include(lr => lr.Item)
                                    .Include(lr => lr.Loan)
                                    .Include(lr => lr.Employee)
                                    .FirstOrDefaultAsync(lr => lr.RequestId==ConvRequestId);
            if (loanRequest == null)
            {
                Console.WriteLine("this line1");
                return false; // Request not found
            }
            var _tenure = loanRequest.Loan.DurationInYears;


            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // Create a new EmployeeIssueDetails record
                var issueDetails = new EmployeeIssueDetail
                {
                    EmployeeId = loanRequest.EmployeeId,
                    ItemId = loanRequest.ItemId,
                    IssueDate = DateTime.UtcNow,
                    ReturnDate = DateTime.UtcNow.AddYears((int)_tenure)
                };

                _context.EmployeeIssueDetails.Add(issueDetails);

                // Create a new EmployeeCardDetails record
                var cardDetails = new EmployeeCardDetail
                {
                    EmployeeId = loanRequest.EmployeeId,
                    LoanId = loanRequest.LoanId,
                    CardIssueDate = DateTime.UtcNow,
                };

                _context.EmployeeCardDetails.Add(cardDetails);

                // Update the ItemMaster to mark the item as accepted
                var itemMaster = await _context.ItemMasters.FindAsync(loanRequest.ItemId);

                if (itemMaster != null)
                {
                    itemMaster.IssueStatus = "issued"; // Update the status as needed
                    _context.Entry(itemMaster).State = EntityState.Modified;
                }

                // Remove the loan request record
                //_context.LoanRequests.Remove(loanRequest);

                // Save changes to the database
                await _context.SaveChangesAsync();

                transaction.Commit();
                return true; // Approval successful
            }
            catch (Exception)
            {
                Console.WriteLine("This is line2");
                transaction.Rollback();
                return false; // Approval failed
            }
        }

        public async Task<bool> DeclineLoanRequestAsync(string requestId)
        {
            Guid ConvRequestId;
            Guid.TryParse(requestId, out ConvRequestId);
            var loanRequest = await _context.LoanRequests
                .Include(lr => lr.Employee)
                .Include(lr => lr.Item)
                .Include(lr => lr.Loan)
                .FirstOrDefaultAsync(lr => lr.RequestId == ConvRequestId);

            if (loanRequest == null)
            {
                return false; // Request not found
            }

            var _tenure = loanRequest.Loan.DurationInYears;
            if (loanRequest == null)
            {
                return false; // Request not found
            }

            var itemMaster = await _context.ItemMasters.FindAsync(loanRequest.ItemId);

            if (itemMaster != null)
            {
                itemMaster.IssueStatus = "rejected"; // Update the status as needed
                _context.ItemMasters.Update(itemMaster);
            }
            await _context.SaveChangesAsync();
            return true;
        }
}
}
