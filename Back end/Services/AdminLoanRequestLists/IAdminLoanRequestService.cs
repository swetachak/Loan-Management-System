using LMS.Models;

namespace LMS.Services
{
    public interface IAdminLoanRequestService
    {
        Task<IEnumerable<LoanRequestDto>> GetPendingLoanRequestsAsync();
        Task<bool> ApproveLoanRequestAsync(string requestId);
        Task<bool> DeclineLoanRequestAsync(string requestId);
    }
}
