namespace Entities
{
    public class Esp8266Entity
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Note { get; set; }
        public ICollection<DeviceDriverEntity>? DeviceDrivers { get; set; }
        public ICollection<InstrumentationEntity>? Instrumentations { get; set; }
    }
}