namespace Models
{
    public class InstrumentationTypeDisplayModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? Unit { get; set; }
        public string? Manufacturer { get; set; }
        public string? ImageUrl { get; set; }
    }
}