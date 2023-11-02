namespace Models
{
    public class InstrumentSetThresholdUpdateModel
    {
        public int Id { get; set; }

        public int? DeviceDriverId { get; set; }

        public int? InstrumentationId { get; set; }

        public bool? OnInUpperThreshold { get; set; }
        // Case: Device return value
        public double? ThresholdValueOn { get; set; }
        public double? ThresholdValueOff { get; set; }
    }
}