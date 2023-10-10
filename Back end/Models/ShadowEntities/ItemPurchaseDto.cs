namespace LMS.Models
{
    public class ItemPurchaseDto
    {
        public Guid IssueId { get; set; }
        public string ItemDescription { get; set; }
        public string ItemMake { get; set; }
        public string ItemCategory { get; set; }
        public int ItemValuation { get; set; }
    }
}
