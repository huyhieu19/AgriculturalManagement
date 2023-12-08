// Ignore Spelling: Mqtt

using Common.Enum;

namespace Entities.Module
{
    public class ModuleEntity
    {
        public Guid Id { get; set; }
        public ModuleType ModuleType { get; set; }
        // FK - User
        public string? UserId { get; set; }
        public UserEntity? User { get; set; }
        public string? Name { get; set; }
        public DateTime DateCreated { get; set; }
        public string? NameRef { get; set; }
        public string? Note { get; set; }
        public string MqttServer { get; set; } = null!;
        public int MqttPort { get; set; }
        public string? ClientId { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public ICollection<DeviceEntity>? Devices { get; set; }
    }
}