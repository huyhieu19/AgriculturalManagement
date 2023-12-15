using Entities.Module;

namespace Entities
{
    public class TimerDeviceEntity
    {
        // PK
        public int Id { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public string? Note { get; set; }
        public DateTime? ShutDownTimer { get; set; }
        public DateTime? OpenTimer { get; set; }
        public bool IsSuccessON { get; set; } = false;
        public bool IsSuccessOFF { get; set; } = false;
        public bool IsRemove { get; set; } = false;
        // FK - Devices
        public Guid DeviceId { get; set; }
        public DeviceEntity Devices { get; set; } = null!;
    }
}