using Common.Enum;

namespace Entities.ESP
{
    public class DeviceTypeOnEspEntity
    {
        public Guid Id { get; set; }
        public Guid EspId { get; set; }
        public EspEntity? Esp { get; set; }
        public string? Name { get; set; }
        public string? Gpio { get; set; }
        public bool IsAction { get; set; }
        public SensorType? DeviceType { get; set; }
        public Guid Topic { get; set; }
        public ResponseSensorType? ResponseType { get; set; }
        public DeviceDriverEntity DeviceDriver { get; set; }
        public InstrumentationEntity Instrumentation { get; set; }
    }
}
