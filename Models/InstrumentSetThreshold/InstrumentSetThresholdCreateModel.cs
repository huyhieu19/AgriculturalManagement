using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class InstrumentSetThresholdCreateModel
    {

        public Guid DeviceDriverId { get; set; }
        public Guid InstrumentationId { get; set; }
        // Case: Device return value
        [Required]
        public int? ThresholdValueOn { get; set; }
        [Required]
        public int? ThresholdValueOff { get; set; }
        [Required]
        public bool? OnInUpperThreshold { get; set; }
    }
}