using Entities.ESP;

namespace Entities
{
    public class TimerDeviceDriverEntity
    {
        public int Id { get; set; }
        public DateTime? DateCreated { get; set; } = DateTime.Now;
        public string? Note { get; set; }

        public DateTime? ShutDownTimer { get; set; }
        public DateTime? OpenTimer { get; set; }

        // Case: Detect rain, ....
        public bool? IsAffected { get; set; }
        public bool IsSuccess { get; set; } = false;
        public bool IsRemove { get; set; } = false;
        public Guid DeviceDriverId { get; set; }
        public DeviceEntity DeviceDriver { get; set; } = null!;
    }
}