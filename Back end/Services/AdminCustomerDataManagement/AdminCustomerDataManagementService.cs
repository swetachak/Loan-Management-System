using LMS.Data;
using LMS.Models;
using System.Runtime.InteropServices;

namespace LMS.Services
{
    public class AdminCustomerDataManagementService : IAdminCustomerDataManagementService
    {
        private readonly AdminCustomerDataManagementProvider _provider;

        public AdminCustomerDataManagementService(AdminCustomerDataManagementProvider provider)
        {
            _provider=provider;
        }

        public async Task<EmployeeMaster> GetEmployeeByIDAsync(string EmployeeID)
        { 
            var _employee =await _provider.GetEmployeeByIDAsync(EmployeeID);
            return _employee;
        }
        public async Task<EmployeeMaster> AddCustomerAsync(EmployeeMaster employee) 
        {
            var _employee =await _provider.AddCustomerAsync(employee);
            return _employee;
        }
        public async Task<List<EmployeeMaster>> GetAllEmployeesAsync()
        { 
            return await _provider.GetAllEmployeesAsync();
        }
        public async Task<EmployeeMaster> UpdateEmployeeAsync(string EmployeeID, EmployeeMaster updatedEmployee)
        { 
            var _employee = await _provider.GetEmployeeByIDAsync(EmployeeID);
            if(_employee == null)
            {
                throw new Exception($"Employee with ID {EmployeeID} not found.");
            }
            _employee.DateOfBirth=updatedEmployee.DateOfBirth;
            _employee.Designation=updatedEmployee.Designation;
            _employee.EmployeeName=updatedEmployee.EmployeeName;    
            _employee.Department=updatedEmployee.Department;    
            _employee.Gender=updatedEmployee.Gender;    
            await _provider.UpdateEmployeeAsync(_employee);
            return _employee;
         }
        public async Task DeleteEmployeeByIDAsync(string EmployeeID)
        {
            await _provider.DeleteEmployeeByIDAsync(EmployeeID);
        }
    }
}
