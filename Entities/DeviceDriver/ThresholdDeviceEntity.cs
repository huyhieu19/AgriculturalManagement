using Entities.Module;

namespace Entities
{
    public class ThresholdDeviceEntity
    {
        public int Id { get; set; }
        public Guid DeviceDriverId { get; set; }
        public DeviceEntity? DeviceDriver { get; set; }
        public Guid InstrumentationId { get; set; }
        public DeviceEntity? DeviceInstrumentation { get; set; }
        public bool? OnInUpperThreshold { get; set; }
        // Case: Device return value
        public double? ThresholdValueOn { get; set; }
        public double? ThresholdValueOff { get; set; }
        public bool IsDelete { get; set; } = false;
    }
}