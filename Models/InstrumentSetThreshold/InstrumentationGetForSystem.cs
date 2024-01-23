using Common.Enum;

namespace Models.InstrumentSetThreshold
{
    public class InstrumentationGetForSystem
    {
        public int Id { get; set; }
        public Guid DeviceDriverId { get; set; }
        public FunctionDeviceType NameRefSensor { get; set; }
        public string? NameDeviceDriver { get; set; }
        public Guid ModuleDriverId { get; set; }
        public Guid ModuleSensorId { get; set; }
        public Guid InstrumentationId { get; set; }
        public bool OnInUpperThreshold { get; set; }
        public bool DeviceDriverAction { get; set; }
        public bool AutoDevice { get; set; }

        // Case: Device return value
        public double? ThresholdValueOn { get; set; }
        public double? ThresholdValueOff { get; set; }
        public bool IsDelete { get; set; } = false;
    }
}
