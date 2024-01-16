namespace Models
{
    public class InstrumentSetThresholdDisplayModel
    {
        public int Id { get; set; }
        public Guid DeviceDriverId { get; set; }
        public Guid InstrumentationId { get; set; }
        public bool OnInUpperThreshold { get; set; }
        public bool DeviceDriverAction { get; set; }
        public string? DeviceInstrumentationName { get; set; }
        public string? DeviceDriverName { get; set; }
        // Case: Device return value
        public int? ThresholdValueOn { get; set; }
        public int? ThresholdValueOff { get; set; }
        public bool IsDelete { get; set; } = false;
        public bool? AutoDevice { get; set; } = false;
    }
}