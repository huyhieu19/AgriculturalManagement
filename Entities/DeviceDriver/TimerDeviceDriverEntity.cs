namespace Entities
{
    public class TimerDeviceDriverEntity
    {
        public int Id { get; set; }
        public DateTime? DateCreated { get; set; } = DateTime.Now;
        public bool? IsDaily { get; set; } = false;
        public string? Note { get; set; }

        public DateTime? ShutDownTimer { get; set; }
        public DateTime? OpenTimer { get; set; }

        // Case: Detect rain, ....
        public bool? IsAffected { get; set; }
        public bool IsSuccess { get; set; } = false;
        public bool IsRemove { get; set; } = false;
        public int DeviceDriverId { get; set; }
        public DeviceDriverEntity DeviceDriver { get; set; } = null!;
    }
}
