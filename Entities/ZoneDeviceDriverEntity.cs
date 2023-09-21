using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    public class ZoneDeviceDriverEntity
    {
        [Key]
        public int Id { get; set; }
        public bool? IsDaily { get; set; } = false;
        public bool? IsAuto { get; set; } = false;
        [ForeignKey("DeviceDriver")]
        public int? DeviceDriverId { get; set; }
        [ForeignKey("Zone")]
        public int? ZoneId { get; set; }
        public int? ShutDownTime { get; set; }
        public int? OpenTimer { get; set; }
        public DeviceDriverEntity? DeviceDriver { get; set; }
        public ZoneEntity? Zone { get; set; }

    }
}
