using LMS.Models;
using LMS.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Controllers.AdminControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminCustomerDataManagementController : ControllerBase
    {
        private readonly IAdminCustomerDataManagementService _service;

        public AdminCustomerDataManagementController(IAdminCustomerDataManagementService service)
        {
            _service=service;
        }
        [HttpGet]
        public async Task<ActionResult<List<EmployeeMaster>>> GetAllEmployeesAsync()
        { 
            return await _service.GetAllEmployeesAsync();
        }
        [HttpPost]
        public async Task<IActionResult> AddCustomerAsync(EmployeeMaster e) 
        {
            try
            {
                var _employee = await _service.AddCustomerAsync(e);
                var _id = _employee.EmployeeId;
                if (_service.GetEmployeeByIDAsync(_id.ToString())!=null)
                {
                    return Ok(_employee);
                }
                else 
                {
                    return NotFound();
                }
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteEmployeeByIDAsync(string EmployeeID)
        {
            if (await _service.GetEmployeeByIDAsync(EmployeeID)!=null)
            {
                try
                {
                    await _service.DeleteEmployeeByIDAsync(EmployeeID);
                    return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }

            }
            else 
            {
                return NotFound("Record Not Found");
            }
       
        }
        [HttpPut]
        public async Task<IActionResult> UpdateEmployeeAsync(string EmployeeID, EmployeeMaster employee)
        {
            try
            {
                var _employee = await _service.UpdateEmployeeAsync(EmployeeID, employee);
                if (_employee==null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(_employee);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
