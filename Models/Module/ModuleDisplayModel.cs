// Ignore Spelling: Mqtt
using Common.Enum;
using Models.Device;

namespace Models
{
    public class ModuleDisplayModel
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public ModuleType ModuleType { get; set; }
        public DateTime DateCreated { get; set; }
        public string? Note { get; set; }
        public string MqttServer { get; set; } = null!;
        public int MqttPort { get; set; }
        public string ClientId { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public List<DeviceDisplayModel>? Devices { get; set; }
    }
}