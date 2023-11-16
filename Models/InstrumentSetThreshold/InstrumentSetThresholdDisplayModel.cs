namespace Models
{
    public class InstrumentSetThresholdDisplayModel
    {
        public int Id { get; set; }

        public Guid DeviceDriverId { get; set; }

        public int InstrumentationId { get; set; }

        public bool OnInUpperThreshold { get; set; }
        // Case: Device return value
        public double? ThresholdValueOn { get; set; }
        public double? ThresholdValueOff { get; set; }
        public bool IsDelete { get; set; } = false;
    }
}