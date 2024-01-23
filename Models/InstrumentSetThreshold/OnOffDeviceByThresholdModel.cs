using Common.Enum;

namespace Models.InstrumentSetThreshold
{
    public class OnOffDeviceByThresholdModel
    {
        public Guid ModuleId { get; set; }
        public Guid DeviceId { get; set; }
        public string? DeviceName { get; set; }
        public string? ValueSensor { get; set; }
        public DeviceType DeviceType { get; set; }
        public string DeviceNameNumber { get; set; } = null!;
        public bool RequestOn { get; set; }
        public int ThresholdId { get; set; }
    }
}
