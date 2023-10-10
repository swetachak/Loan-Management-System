using LMS.Models;
using Microsoft.EntityFrameworkCore;

namespace LMS.Repository
{
    public class EmployeeManagementProvider : IEmployeeManagementProvider
    {
        private readonly Lms3Context _context;
        public EmployeeManagementProvider(Lms3Context context)
        {
            _context = context;
        }

        public async Task<List<ItemPurchaseDto>> GetItemPurchasedByEmployeeIdAsync(string employeeId)
        {
            //{0} is place holder employeeId goes into this
            string sqlQuery = @"
            SELECT e.issue_id as IssueId,i.item_description as ItemDescription,i.item_make as ItemMake,i.item_category as ItemCategory,i.item_valuation as ItemValuation
            FROM employee_issue_details e
            INNER JOIN item_master i ON e.item_id = i.item_id
            WHERE e.employee_id = {0}";

            Guid ConvemployeeId;
            Guid.TryParse(employeeId, out ConvemployeeId);
            // Use FromSqlRaw to execute the raw SQL query
            return await _context.ItemPurchaseDtos
                .FromSqlRaw(sqlQuery, ConvemployeeId)
                .ToListAsync();
        }
        public async Task<List<LoanCardMaster>> GetLoanCardsByEmployeeIdAsync(string employeeId)
        {
            // Query the database to retrieve loan card details for the given employee
            var loanCards = await _context.EmployeeCardDetails
                .Where(e => e.EmployeeId.ToString() == employeeId)
                .Select(e => e.Loan)
                .ToListAsync();

            return loanCards;
        }
    }
}
