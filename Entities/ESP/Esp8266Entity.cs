namespace Entities
{
    public class Esp8266Entity
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string MqttServer { get; set; } = null!;
        public int MqttPort { get; set; }
        public string ClientId { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Note { get; set; } = null!;
        public ICollection<DeviceDriverEntity>? DeviceDrivers { get; set; }
        public ICollection<InstrumentationEntity>? Instrumentations { get; set; }
    }
}