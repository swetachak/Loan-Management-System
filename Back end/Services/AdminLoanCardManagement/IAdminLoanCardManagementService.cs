using LMS.Models;

namespace LMS.Services
{
    public interface IAdminLoanCardManagementService
    {
        Task<LoanCardMaster> GetLoanCardByIdAsync(string loanId);

        Task AddLoanCardAsync(LoanCardMaster loanCard);
        Task<List<LoanCardMaster>> GetAllLoanCardsAsync();
        Task UpdateLoanCardAsync(string loanId, LoanCardMaster updatedLoanCard);
        Task DeleteLoanCardAsync(string loanId);
    }
}
