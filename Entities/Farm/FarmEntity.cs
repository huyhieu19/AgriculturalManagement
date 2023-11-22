namespace Entities.Farm
{
    public class FarmEntity
    {
        // PK
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? Address { get; set; }
        public double? Area { get; set; }
        public string? Note { get; set; }
        public DateTime? DateCreated { get; set; } = DateTime.UtcNow;
        // FK - User
        public string UserId { get; set; } = null!;
        public UserEntity? User { get; set; }
        public ICollection<ZoneEntity>? Zones { get; set; }
    }
}