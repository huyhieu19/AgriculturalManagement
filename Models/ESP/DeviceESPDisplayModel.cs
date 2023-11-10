using Common.Enum;

namespace Models.ESP
{
    public class DeviceESPDisplayModel
    {
        public Guid Id { get; set; }
        public Guid EspId { get; set; }
        public string? Name { get; set; }
        public string? Gpio { get; set; }
        public bool IsUsed { get; set; }
        public SensorType? DeviceType { get; set; }
        public Guid Topic { get; set; }
        public ResponseSensorType? ResponseType { get; set; }
        public DeviceDriverDisplayModel? DeviceDriver { get; set; }
        public InstrumentationDisplayModel? Instrumentation { get; set; }
    }
}
