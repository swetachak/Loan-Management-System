using LMS.Data;
using LMS.Models;
using LMS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Diagnostics;

namespace LMS.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _config;
        private readonly EmployeeProvider _employeeDataProvider;
        public AuthService(IConfiguration config, EmployeeProvider employeeDataProvider)
        {
            _config = config;
            _employeeDataProvider = employeeDataProvider;
        }
        public EmployeeCredential GetEmployeeDetail(EmployeeViewModel login)
        {
            EmployeeCredential employee = null;
            employee = _employeeDataProvider.GetEmployeeDetail(login);
            return employee;
        }

        public string GenerateJSONWebToken(EmployeeCredential employeeInfo)
        {

            if (employeeInfo is null)
            {
                throw new ArgumentNullException(nameof(employeeInfo));
            }
            List<Claim> claims = new List<Claim>();
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            claims.Add(new Claim("Username", employeeInfo.EmployeeEmail));
            if (employeeInfo.EmployeeRole.Equals("admin"))
            {
                claims.Add(new Claim("role", "admin"));
            }
            else
            {
                claims.Add(new Claim("role", "customer"));

            }
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              claims,
              expires: DateTime.Now.AddMinutes(2),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public EmployeeCredential AuthenticateEmployee(EmployeeViewModel login)
        {
            EmployeeCredential employee = _employeeDataProvider.GetEmployeeDetail(login);
            return employee;
        }

        public Boolean RegisterEmployee(string employeeId,EmployeeCredential e)
        {
            return _employeeDataProvider.RegisterEmployee(employeeId,e); 
        }
    }
}