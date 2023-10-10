using LMS.Data;
using LMS.Models;

namespace LMS.Services
{
    public class AdminLoanRequestService : IAdminLoanRequestService
    {
        private readonly AdminLoanRequestListProvider _provider;
        public AdminLoanRequestService(AdminLoanRequestListProvider provider)
        {
            _provider=provider;
        }

        public async Task<IEnumerable<LoanRequestDto>> GetPendingLoanRequestsAsync()
        {
            return await _provider.GetPendingLoanRequestsAsync();
        }
        public async Task<bool> ApproveLoanRequestAsync(string requestId)
        {
            return await _provider.ApproveLoanRequestAsync(requestId);
        }
        public async Task<bool> DeclineLoanRequestAsync(string requestId)
        {
            return await _provider.DeclineLoanRequestAsync(requestId);
        }
    }
}
