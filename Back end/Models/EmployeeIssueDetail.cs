using System;
using System.Collections.Generic;

namespace LMS.Models;

public partial class EmployeeIssueDetail
{
    public Guid IssueId { get; set; }

    public Guid? EmployeeId { get; set; }

    public Guid? ItemId { get; set; }

    public DateTime? IssueDate { get; set; }

    public DateTime? ReturnDate { get; set; }

    public virtual EmployeeMaster? Employee { get; set; }

    public virtual ItemMaster? Item { get; set; }
}
