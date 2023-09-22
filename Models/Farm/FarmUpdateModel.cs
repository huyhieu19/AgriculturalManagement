namespace Models.Farm
{
    public class FarmUpdateModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? Address { get; set; }
        public string? Note { get; set; }
    }
}
