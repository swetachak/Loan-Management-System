using LMS.Models;
using LMS.Services;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeManagementController : ControllerBase
    {
        private readonly IEmployeeManagementService _service;

        public EmployeeManagementController(IEmployeeManagementService service)
        {
            _service=service;
        }
        [HttpGet("GetAllLoansByID/{employeeId}")]
        public async Task<ActionResult<List<LoanCardMaster>>> GetLoanCardsByEmployeeId(string employeeId)
        {
            var loanCards = await _service.GetLoanCardsByEmployeeIdAsync(employeeId);

            if (loanCards == null || loanCards.Count == 0)
            {
                return NotFound();
            }

            return Ok(loanCards);
        }
        [HttpGet("DisplayAllItemsPurchasedByID/{employeeId}")]
        public async Task<ActionResult<List<ItemPurchaseDto>>> DisplayAllItemsPurchasedById(string employeeId)
        {
            var itemsPurchased = await _service.DisplayItemPurchasedBYEmployeeIdAsync(employeeId);
            if (itemsPurchased == null || itemsPurchased.Count == 0)
            {
                return NotFound();
            }

            return Ok(itemsPurchased);
        }

    }
}
