using Common.Enum;

namespace Models.Device
{
    public class DeviceDisplayModel
    {
        public Guid Id { get; set; }
        public Guid ModuleId { get; set; }
        public int? ZoneId { get; set; }
        public string? Name { get; set; }
        public string? NameRef { get; set; }
        public string? Description { get; set; }
        public DateTime? DateCreated { get; set; }
        public bool IsAction { get; set; }
        public bool IsUsed { get; set; }
        public bool IsAuto { get; set; }
        public string? Unit { get; set; }
        public string? Gpio { get; set; }
        public Guid Topic { get; set; }
        public DeviceType DeviceType { get; set; }
        public ResponseSensorType? ResponseType { get; set; }
    }
}
