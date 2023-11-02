namespace Models
{
    public class EspDisplayModel
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Note { get; set; }
        public string MqttServer { get; set; } = null!;
        public int MqttPort { get; set; }
        public string ClientId { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public List<DeviceDriverDisplayModel>? DeviceDrivers { get; set; }
        public List<InstrumentationDisplayModel>? Instrumentations { get; set; }
    }
}