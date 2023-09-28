using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    public class TimerDeviceDriverEntity
    {
        public int Id { get; set; }
        public bool? IsDaily { get; set; } = false;
        public bool IsAuto { get; set; } = false;
        public DateTime ShutDownTimer { get; set; }
        public DateTime OpenTimer { get; set; }
        [ForeignKey("DeviceDriver")]
        public int? DeviceDriverId { get; set; }
        public bool? IsRemove { get; set; } = false;
        public DeviceDriverEntity? DeviceDriver { get; set; }
    }
}
