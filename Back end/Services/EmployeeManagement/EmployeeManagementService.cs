using LMS.Models;
using LMS.Repository;

namespace LMS.Services
{
    public class EmployeeManagementService : IEmployeeManagementService
    {
        private readonly EmployeeManagementProvider _provider;

        public EmployeeManagementService(EmployeeManagementProvider provider)
        { 
            _provider = provider;
        }
        public async Task<List<ItemPurchaseDto>> DisplayItemPurchasedBYEmployeeIdAsync(string employeeId)
        {
            var itemsPurchased = await _provider.GetItemPurchasedByEmployeeIdAsync(employeeId);

            return itemsPurchased.ToList();
        }
        public async Task<List<LoanCardMaster>> GetLoanCardsByEmployeeIdAsync(string employeeId)
        {
            return await _provider.GetLoanCardsByEmployeeIdAsync(employeeId);
        }
    }
}
