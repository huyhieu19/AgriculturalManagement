using Entities.Module;

namespace Entities
{
    public class TimerDeviceDriverEntity
    {
        public int Id { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public string? Note { get; set; }

        public DateTime? ShutDownTimer { get; set; }
        public DateTime? OpenTimer { get; set; }

        // Case: Detect rain, ....
        public bool? IsAffected { get; set; }
        public bool IsSuccess { get; set; } = false;
        public bool IsRemove { get; set; } = false;
        public Guid DeviceDriverId { get; set; }
        public DeviceEntity Devices { get; set; } = null!;
    }
}