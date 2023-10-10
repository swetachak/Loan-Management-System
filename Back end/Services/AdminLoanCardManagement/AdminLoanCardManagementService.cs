using LMS.Data;
using LMS.Models;

namespace LMS.Services
{
    public class AdminLoanCardManagementService : IAdminLoanCardManagementService
    {
        private readonly AdminLoanCardManagementProvider _provider;

        public AdminLoanCardManagementService(AdminLoanCardManagementProvider provider)
        {
            _provider = provider;
        }

        public async Task<LoanCardMaster> GetLoanCardByIdAsync(string loanId)
        {
            return await _provider.GetLoanCardByIdAsync(loanId);
        }

        public async Task AddLoanCardAsync(LoanCardMaster loanCard)
        { 
            await _provider.AddLoanCardAsync(loanCard);
        }
        public async Task<List<LoanCardMaster>> GetAllLoanCardsAsync()
        { 
            return await _provider.GetAllLoanCardsAsync();
        }

        public async Task UpdateLoanCardAsync(string loanId, LoanCardMaster updatedLoanCard)
        {
            var existingLoanCard = await _provider.GetLoanCardByIdAsync(loanId);

            if (existingLoanCard == null)
            {
                throw new Exception($"Loan card with ID {loanId} not found.");
            }

            existingLoanCard.LoanType = updatedLoanCard.LoanType;
            existingLoanCard.DurationInYears = updatedLoanCard.DurationInYears;

            await _provider.UpdateLoanCardAsync(existingLoanCard);
        }
        public async Task DeleteLoanCardAsync(string loanId)
        {
            await _provider.DeleteLoanCardAsync(loanId);
        }
    }
}



