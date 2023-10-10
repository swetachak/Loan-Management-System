namespace LMS.Models
{
    public class LoanRequestDto
    {
        public string EmployeeName { get; set; } = null!;

        public string? Designation { get; set; }

        public string? Department { get; set; }

        public string? Gender { get; set; }
        public string? ItemDescription { get; set; }
        public string? ItemMake { get; set; }
        public string? ItemCategory { get; set; }
        public int? ItemValuation { get; set; }
        public string RequestId { get; set; }
    }
}
