using Common.Enum;
using Models.Device;

namespace Models.DeviceTimer
{
    public class TimerDeviceDriverDisplayModel
    {
        public int Id { get; set; }
        public string? DeviceName { get; set; }
        public DateTime? DateCreated { get; set; } = null;
        public DateTime? DateUpdated { get; set; } = null;
        public string? Note { get; set; }

        public DateTime? ShutDownTimer { get; set; } = null;
        public DateTime? OpenTimer { get; set; } = null;

        public bool? IsAffected { get; set; }
        public bool IsSuccessON { get; set; } = false;
        public bool IsSuccessOFF { get; set; } = false;
        public bool IsRemove { get; set; } = false;
        public bool IsAuto { get; set; }
        public DeviceType DeviceType { get; set; }
        public FunctionDeviceType NameRef { get; set; }
        public Guid DeviceId { get; set; }
        public Guid ModuleId { get; set; }
        public DeviceDisplayModel? DeviceModel { get; set; }
    }
}