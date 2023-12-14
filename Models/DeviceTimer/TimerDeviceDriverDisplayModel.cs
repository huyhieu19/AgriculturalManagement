using Common.Enum;
using Models.Device;

namespace Models.DeviceTimer
{
    public class TimerDeviceDriverDisplayModel
    {
        public int Id { get; set; }
        public string? DeviceName { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public string? Note { get; set; }

        public DateTime? ShutDownTimer { get; set; }
        public DateTime? OpenTimer { get; set; }

        public bool? IsAffected { get; set; }
        public bool IsSuccessON { get; set; } = false;
        public bool IsSuccessOFF { get; set; } = false;
        public bool IsRemove { get; set; } = false;
        public bool IsAuto { get; set; }
        public DeviceType DeviceType { get; set; }
        public string NameRef { get; set; } = null!;
        public Guid DeviceId { get; set; }
        public DeviceDisplayModel? DeviceModel { get; set; }
    }
}