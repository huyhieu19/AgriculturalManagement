namespace Entities.ESP
{
    public class EspEntity
    {
        public Guid Id { get; set; }
        public string? UserId { get; set; }
        public UserEntity? User { get; set; }
        public string? Name { get; set; }
        public DateTime DateCreated { get; set; }
        public string? Note { get; set; }
        public Guid Topic { get; set; }
        public string MqttServer { get; set; } = null!;
        public int MqttPort { get; set; }
        public string? ClientId { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public ICollection<DeviceEntity>? DeviceTypes { get; set; }
    }
}