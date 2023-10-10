using LMS.Models;

namespace LMS.Services
{
    public interface IAdminCustomerDataManagementService
    {
        Task<EmployeeMaster> GetEmployeeByIDAsync(string EmployeeID);
        Task<EmployeeMaster> AddCustomerAsync(EmployeeMaster employee);

        Task<List<EmployeeMaster>> GetAllEmployeesAsync();
        Task<EmployeeMaster> UpdateEmployeeAsync(string EmployeeID, EmployeeMaster updatedEmployee);
        Task DeleteEmployeeByIDAsync(string EmployeeID);
    }
}
