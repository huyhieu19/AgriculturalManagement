﻿using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class InstrumentSetThresholdCreateModel
    {

        public Guid DeviceDriverId { get; set; }
        public Guid InstrumentationId { get; set; }
        public string TypeDevice { get; set; } = null!;
        // Case: Device return value
        [Required]
        public double? ThresholdValueOn { get; set; }
        [Required]
        public double? ThresholdValueOff { get; set; }
        [Required]
        public bool? OnInUpperThreshold { get; set; }
    }
}