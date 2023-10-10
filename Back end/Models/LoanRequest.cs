using System;
using System.Collections.Generic;

namespace LMS.Models;

public partial class LoanRequest
{
    public Guid EmployeeId { get; set; }

    public Guid ItemId { get; set; }

    public Guid LoanId { get; set; }

    public Guid RequestId { get; set; }

    public virtual EmployeeMaster Employee { get; set; } = null!;

    public virtual ItemMaster Item { get; set; } = null!;

    public virtual LoanCardMaster Loan { get; set; } = null!;
}
