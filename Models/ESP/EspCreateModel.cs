namespace Models
{
    public class EspCreateModel
    {
        public string? Name { get; set; }
        public string? Note { get; set; }
        public string MqttServer { get; set; } = null!;
        public int MqttPort { get; set; }
        public string? ClientId { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public Guid Topic { get; set; }
    }
}