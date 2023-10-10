using LMS.Models;
using Microsoft.EntityFrameworkCore;

namespace LMS.Data
{
    public class AdminLoanCardManagementProvider : IAdminLoanCardManagementProvider
    {
        private readonly Lms3Context _context;

        public AdminLoanCardManagementProvider(Lms3Context context)
        {
            _context = context;
        }
        public async Task<LoanCardMaster> GetLoanCardByIdAsync(string loanId)
        {
            Guid convLoanId;
            Guid.TryParse(loanId, out convLoanId);
            return await _context.LoanCardMasters.FindAsync(convLoanId);
        }
        public async Task<LoanCardMaster> GetLoanCardByCategoryTenureAsync(string category, int tenure)
        {
            return await _context.LoanCardMasters
                .Where(l => l.LoanType == category && 
                l.DurationInYears == tenure)
                .FirstOrDefaultAsync();
        }
        public async Task AddLoanCardAsync(LoanCardMaster loanCard)
        {
            await _context.LoanCardMasters.AddAsync(loanCard);
            await _context.SaveChangesAsync();
        }
        public async Task<List<LoanCardMaster>> GetAllLoanCardsAsync()
        {
            return await _context.LoanCardMasters.ToListAsync();
        }
        public async Task UpdateLoanCardAsync(LoanCardMaster loanCard)
        {
            _context.Entry(loanCard).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteLoanCardAsync(string loanId)
        {
            Guid convLoanId;
            Guid.TryParse(loanId, out convLoanId);
            var _loancard = await _context.LoanCardMasters.FindAsync(convLoanId);

            var sqlQuery = @"select * from employee_card_details as e " +
                "where e.loan_id = {0};";


            var _employeeCardDetails = await _context.EmployeeCardDetails
                                               .FromSqlRaw(sqlQuery, convLoanId)
                                               .ToListAsync();
            if (_employeeCardDetails != null)
            {
                _context.EmployeeCardDetails.RemoveRange(_employeeCardDetails);
            }
            if (_loancard != null) 
            {
                _context.LoanCardMasters.Remove(_loancard);
                await _context.SaveChangesAsync();
            }
        }
    }
}
