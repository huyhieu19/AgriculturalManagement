namespace Entities
{
    public class DeviceInstrumentOnOffEntity
    {
        public int Id { get; set; }

        public int? DeviceDriverId { get; set; }
        public DeviceDriverEntity? DeviceDriver { get; set; }
        public int? InstrumentationId { get; set; }
        public InstrumentationEntity? Instrumentation { get; set; }

        // Case: Device return value
        public double? ThresholdValueOn { get; set; }
        public double? ThresholdValueOff { get; set; }
        public bool IsDelete { get; set; } = false;
    }
}
