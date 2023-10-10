using LMS.Models;
using Microsoft.EntityFrameworkCore;

namespace LMS.Data
{
    public class AdminCustomerDataManagementProvider : IAdminCustomerDataManagementProvider
    {
        private readonly Lms3Context _context;

        public AdminCustomerDataManagementProvider(Lms3Context context)
        {
            _context = context;
        }
        public async Task<EmployeeMaster> GetEmployeeByIDAsync(string EmployeeID)
        {
            Guid convEmpID;
            Guid.TryParse(EmployeeID, out convEmpID);
            return await _context.EmployeeMasters.FindAsync(convEmpID);
        }
        public async Task<EmployeeMaster> AddCustomerAsync(EmployeeMaster employee)
        {
            
            await _context.EmployeeMasters.AddAsync(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task<List<EmployeeMaster>> GetAllEmployeesAsync()
        {
           var _employees =await _context.EmployeeMasters.ToListAsync();
            return _employees;
        }
        public async Task DeleteEmployeeByIDAsync(string EmployeeID)
        {
           
                Guid convEmployeeId;
                Guid.TryParse(EmployeeID, out convEmployeeId);
                var _employee = await _context.EmployeeMasters.FindAsync(convEmployeeId);
                if (_employee != null)
                {
                _context.EmployeeMasters.Remove(_employee);
                await _context.SaveChangesAsync();
                }
            
        }
        public async Task UpdateEmployeeAsync(EmployeeMaster updatedEmployee)
        {
            _context.Entry(updatedEmployee).State = EntityState.Modified;
            await _context.SaveChangesAsync();

        }

    }
}
