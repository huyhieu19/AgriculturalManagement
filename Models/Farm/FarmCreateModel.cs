namespace Models
{
    public class FarmCreateModel
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public double? Area { get; set; }
        public string? Address { get; set; }
        public string? Note { get; set; }
        public string? UserId { get; set; }
    }
}
