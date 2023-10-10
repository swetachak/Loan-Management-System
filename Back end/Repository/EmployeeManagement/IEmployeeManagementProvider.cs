using LMS.Models;
using System.Threading.Tasks;

namespace LMS.Repository
{
    public interface IEmployeeManagementProvider
    {
        Task<List<ItemPurchaseDto>> GetItemPurchasedByEmployeeIdAsync(string employeeId);
        Task<List<LoanCardMaster>> GetLoanCardsByEmployeeIdAsync(string employeeId);
    }
}
