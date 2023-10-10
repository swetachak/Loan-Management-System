using LMS.Models;
using LMS.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Controllers.ApplyLoanControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _service;

        public CategoryController(ICategoryService service)
        {
            _service = service;
        }
        [HttpGet("Categories")]
        public async Task<IActionResult> GetAllCategoriesAsync()
        {
            try 
            {
                var categories = await _service.GetAllCategoriesAsync();
                return Ok(categories);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("Materials/{CategoryName}")]
        public async Task<IActionResult> GetAllMaterialsByCategoryAsync(string CategoryName)
        {
            try 
            {
                var materials = await _service.GetAllMaterialsByCategoryAsync(CategoryName);
                return Ok(materials);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("Tenures/{CategoryName}")]

        public async Task<IActionResult> GetAvailableTenuresForCategory(string CategoryName)
        {
            try
            {
                var tenures = await _service.GetAvailableTenuresForCategoryAsync(CategoryName);
                return Ok(tenures);
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
