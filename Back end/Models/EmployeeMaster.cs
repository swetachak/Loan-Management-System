using System;
using System.Collections.Generic;

namespace LMS.Models;

public partial class EmployeeMaster
{
    public Guid EmployeeId { get; set; }

    public string EmployeeName { get; set; } = null!;

    public string? Designation { get; set; }

    public string? Department { get; set; }

    public string? Gender { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public DateTime? DateOfJoining { get; set; }

    public virtual ICollection<EmployeeCardDetail> EmployeeCardDetails { get; set; } = new List<EmployeeCardDetail>();

    public virtual ICollection<EmployeeCredential> EmployeeCredentials { get; set; } = new List<EmployeeCredential>();

    public virtual ICollection<EmployeeIssueDetail> EmployeeIssueDetails { get; set; } = new List<EmployeeIssueDetail>();

    public virtual ICollection<LoanRequest> LoanRequests { get; set; } = new List<LoanRequest>();
}
