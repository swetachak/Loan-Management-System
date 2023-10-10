using LMS.Models;
namespace LMS.Data
{
    public interface IAdminLoanRequestListProvider
    {
        Task<List<LoanRequestDto>> GetPendingLoanRequestsAsync();
        Task<bool> ApproveLoanRequestAsync(string requestId);
        Task<bool> DeclineLoanRequestAsync(string requestId);
    }
}
