using System;
using System.Collections.Generic;

namespace LMS.Models;

public partial class LoanCardMaster
{
    public Guid LoanId { get; set; }

    public string? LoanType { get; set; }

    public int? DurationInYears { get; set; }

    public virtual ICollection<EmployeeCardDetail> EmployeeCardDetails { get; set; } = new List<EmployeeCardDetail>();

    public virtual ICollection<LoanRequest> LoanRequests { get; set; } = new List<LoanRequest>();

    public virtual Category? LoanTypeNavigation { get; set; }
}
