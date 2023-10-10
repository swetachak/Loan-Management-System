using LMS.Models;
using LMS.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LMS.Controllers.AdminControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminItemDataManagementController : ControllerBase
    {
        private readonly IItemDataManagementService _service;

        public AdminItemDataManagementController(IItemDataManagementService itemMasterService)
        {
            _service = itemMasterService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemMaster>>> GetItemMasters()
        {
            var itemMasters = await _service.GetItemMastersAsync();
            return Ok(itemMasters);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ItemMaster>> GetItemMaster(string id)
        {
            var itemMaster = await _service.GetItemMasterByIdAsync(id);
            if (itemMaster == null)
            {
                return NotFound();
            }
            return Ok(itemMaster);
        }

        [HttpPost]
        public async Task<IActionResult> CreateItemMaster(ItemMaster itemMaster)
        {
            var createdItemId = await _service.CreateItemMasterAsync(itemMaster);
            if (createdItemId == null)
            {
                return BadRequest();
            }
            return Ok(createdItemId);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItemMaster(string id, ItemMaster itemMaster)
        {
            try
            {
                await _service.UpdateItemMasterAsync(id,itemMaster);
                if (_service.GetItemMasterByIdAsync(id)==null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItemMaster(string id)
        {
            await _service.DeleteItemMasterAsync(id);
            return NoContent();
        }
    }
}
