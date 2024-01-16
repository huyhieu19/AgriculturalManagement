namespace Models
{
    public class InstrumentSetThresholdUpdateModel
    {
        public int Id { get; set; }

        public Guid DeviceDriverId { get; set; }

        public Guid InstrumentationId { get; set; }
        public bool? OnInUpperThreshold { get; set; }
        // Case: Device return value
        public int? ThresholdValueOn { get; set; }
        public int? ThresholdValueOff { get; set; }
    }
}