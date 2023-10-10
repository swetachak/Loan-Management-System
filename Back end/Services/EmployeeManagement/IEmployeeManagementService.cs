using LMS.Models;

namespace LMS.Services
{
    public interface IEmployeeManagementService
    {
        Task<List<LoanCardMaster>> GetLoanCardsByEmployeeIdAsync(string employeeId);
        Task<List<ItemPurchaseDto>> DisplayItemPurchasedBYEmployeeIdAsync(string employeeId);

    }
}
