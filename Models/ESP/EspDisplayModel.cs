namespace Models
{
    public class EspDisplayModel
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Note { get; set; }
        public List<DeviceDriverDisplayModel>? DeviceDrivers { get; set; }
        public List<InstrumentationDisplayModel>? Instrumentations { get; set; }
    }
}
