namespace Entities
{
    public class InstrumentationTypeEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Unit { get; set; }
        public string? Manufacturer { get; set; }
        public string? ImageUrl { get; set; }
        public ICollection<InstrumentationEntity>? Instrumentations { get; set; }
        public InstrumentationTypeEntity(int Id, string Name, string? Unit = null, string? Description = null, string? Manufacturer = null, string? ImageUrl = null)
        {
            this.Id = Id;
            this.Name = Name;
            this.Unit = Unit;
            this.Description = Description;
            this.Manufacturer = Manufacturer;
            this.ImageUrl = ImageUrl;
        }
    }
}
