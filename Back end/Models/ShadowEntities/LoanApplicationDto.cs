namespace LMS.Models
{
    public class LoanApplicationDto
    {
        public string? ItemDescription { get; set; }

        public string? ItemMake { get; set; }

        public string? ItemCategory { get; set; }

        public int? ItemValuation { get; set; }

        public int Tenure { get; set; }
    }
}
