using LMS.Models;

namespace LMS.Services
{
    public interface IApplyForLoanService
    {
        Task<string> ApplyForLoanAsync(LoanApplicationDto application, string employeeId);
    }
}
