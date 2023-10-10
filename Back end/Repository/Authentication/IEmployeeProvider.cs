using LMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.Data
{
    public interface IEmployeeProvider
    {
        public EmployeeCredential GetEmployeeDetail(EmployeeViewModel login);
        public Boolean RegisterEmployee(string employeeId,EmployeeCredential e);
    }
}