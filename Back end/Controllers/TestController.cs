using LMS.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly Lms3Context _db;

        public TestController(Lms3Context db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllEmployees()
        {
            return Ok(_db.EmployeeMasters);
        }
    }
}
