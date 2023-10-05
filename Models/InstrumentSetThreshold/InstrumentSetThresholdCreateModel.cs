namespace Models
{
    public class InstrumentSetThresholdCreateModel
    {

        public int? DeviceDriverId { get; set; }
        public int? InstrumentationId { get; set; }

        // Case: Device return value
        public double? ThresholdValueOn { get; set; }
        public double? ThresholdValueOff { get; set; }
    }
}
