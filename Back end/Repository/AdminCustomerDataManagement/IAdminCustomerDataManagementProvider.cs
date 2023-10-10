using LMS.Models;

namespace LMS.Data
{
    public interface IAdminCustomerDataManagementProvider
    {
        Task<EmployeeMaster> GetEmployeeByIDAsync(string id);
        Task<EmployeeMaster> AddCustomerAsync(EmployeeMaster employee);

        Task<List<EmployeeMaster>> GetAllEmployeesAsync();
        Task UpdateEmployeeAsync(EmployeeMaster updatedEmployee);
        Task DeleteEmployeeByIDAsync(string EmployeeID);
    }
}
