namespace Models
{
    public class TimerDeviceDriverDisplayModel
    {
        public int Id { get; set; }
        public bool? IsDaily { get; set; } = false;
        public DateTime? DateCreated { get; set; }
        public string? Note { get; set; }
        public DateTime? ShutDownTimer { get; set; }
        public DateTime? OpenTimer { get; set; }
        public double? ThresholdValueOn { get; set; }
        public double? ThresholdValueOff { get; set; }
        public bool? IsAffected { get; set; }
        public bool IsSuccess { get; set; } = false;
        public bool IsRemove { get; set; } = false;
        public int? DeviceDriverId { get; set; }
    }
}
