namespace Models
{
    public class TypeTreeDisplayModel
    {
        public int Id { get; set; }
        public string NameType { get; set; } = null!;
        public string? Description { get; set; }
        public string? Note { get; set; }
        public string? ImageUrl { get; set; }
    }
}