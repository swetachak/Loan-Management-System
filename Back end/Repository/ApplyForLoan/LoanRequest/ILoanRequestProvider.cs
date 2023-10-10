using LMS.Models;

namespace LMS.Data
{
    public interface ILoanRequestProvider
    {
        Task<List<LoanRequest>> GetLoanRequestsAsync();
        Task<LoanRequest> GetLoanRequestByIdAsync(string requestId);
        Task<string> CreateLoanRequestAsync(LoanRequest request);
        Task UpdateLoanRequestAsync(LoanRequest request);
        Task DeleteLoanRequestAsync(string requestId);
    }
}
