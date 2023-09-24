namespace Models
{
    public class FarmDisplayModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public string? Description { get; set; }
        public string? Address { get; set; }
        public string? Note { get; set; }
        public string? UserId { get; set; }
    }
}
