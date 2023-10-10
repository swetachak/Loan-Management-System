using LMS.Data;
using LMS.Models;

namespace LMS.Services
{
    public class ApplyForLoanService : IApplyForLoanService
    {
        private readonly LoanRequestProvider _loanRequestProvider;
        private readonly ItemMasterProvider _itemMasterProvider;
        private readonly AdminLoanCardManagementProvider _loanCardProvider;

        public ApplyForLoanService(LoanRequestProvider loanRequestProvider, ItemMasterProvider itemMasterProvider, AdminLoanCardManagementProvider loanCardProvider)
        {
            _loanRequestProvider=loanRequestProvider;
            _itemMasterProvider=itemMasterProvider;
            _loanCardProvider=loanCardProvider; 
        }

        public async Task<string> ApplyForLoanAsync(LoanApplicationDto application,string employeeId) 
        {
            //find the loan_id based on category and tenure
            var _category = application.ItemCategory;
            var _tenure =application.Tenure;
            var _loanCard = await _loanCardProvider.GetLoanCardByCategoryTenureAsync(_category, _tenure);
            if (_loanCard==null)
            {
                return null;
            }
            var _loanId = _loanCard.LoanId;
            //create a record in item master
            var _item = new ItemMaster
            {
                IssueStatus ="waiting",
                ItemCategory = _category,
                ItemDescription = application.ItemDescription,
                ItemMake = application.ItemMake,
                ItemValuation = application.ItemValuation,
            };
            var _itemId = await _itemMasterProvider.CreateItemMasterAsync(_item);
            //cretae a record in loanrequest
            Guid ConvEmployeeId;
            Guid.TryParse(employeeId, out ConvEmployeeId);
            var _loanRequest = new LoanRequest
            {
                EmployeeId = ConvEmployeeId,
                LoanId = _loanId,
                ItemId = _itemId,
            };
            return await _loanRequestProvider.CreateLoanRequestAsync(_loanRequest);
        }
    }
}
