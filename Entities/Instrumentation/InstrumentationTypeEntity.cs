namespace Entities
{
    public class InstrumentationTypeEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? Unit { get; set; }
        //public ICollection<InstrumentationEntity>? Instrumentations { get; set; }
    }
}