using Models.ESP;

namespace Models
{
    public class EspDisplayModel
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public Guid Topic { get; set; }
        public string? Note { get; set; }
        public string MqttServer { get; set; } = null!;
        public int MqttPort { get; set; }
        public string ClientId { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public List<DeviceESPDisplayModel>? DeviceTypes { get; set; }
    }
}