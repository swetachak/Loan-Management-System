using System;
using System.Collections.Generic;

namespace LMS.Models;

public partial class EmployeeCredential
{
    public Guid? EmployeeId { get; set; }

    public string EmployeeEmail { get; set; } = null!;

    public string EmployeePassword { get; set; } = null!;

    public string? EmployeeRole { get; set; }

    public virtual EmployeeMaster? Employee { get; set; }
}
