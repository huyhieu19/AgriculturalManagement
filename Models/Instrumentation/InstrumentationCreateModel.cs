namespace Models
{
    public class InstrumentationCreateModel
    {
        public string Name { get; set; } = null!;
        public string? Note { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; } = false;
        public DateTime? DateOfPurchanse { get; set; }
        public int? ZoneId { get; set; }
    }
}
