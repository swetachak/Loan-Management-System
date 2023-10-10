using LMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.Data
{
    public class EmployeeProvider : IEmployeeProvider
    {
        private readonly Lms3Context _db;

        public EmployeeProvider(Lms3Context db)
        {
            _db = db;
        }

        public EmployeeCredential GetEmployeeDetail(EmployeeViewModel login)
        {
            //return users.SingleOrDefault(x => x.EmployeeId == login.Username && x.EmployeePassword == login.Password);
            return _db.EmployeeCredentials.SingleOrDefault(x => x.EmployeeEmail == login.Username && x.EmployeePassword == login.Password);
        }

        public Boolean RegisterEmployee(string employeeId, EmployeeCredential e)
        {
            try
            {
                Guid ConvEmployeeId;
                Guid.TryParse(employeeId, out ConvEmployeeId);
                e.EmployeeId = ConvEmployeeId;
                _db.EmployeeCredentials.AddAsync(e);
                _db.SaveChangesAsync();
                //_db.EmployeeMasters.Add(e);
                //_db.SaveChanges();
                return true;
            } 
            catch
            {
                return false;
            }
        }

        public List<ItemMaster> GetItemDetails(String id)
        {
            try
            {
                var query1 = from item in _db.ItemMasters
                              join issue in _db.EmployeeIssueDetails
                              on item.ItemId equals issue.ItemId
                              where issue.EmployeeId.ToString() == id
                              select item;

                List<ItemMaster> _items = query1.ToList();
                return _items;
            }
            catch
            {
                return new List<ItemMaster>();
            }
        }
    }
}