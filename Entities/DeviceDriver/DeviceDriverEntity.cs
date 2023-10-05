using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    public class DeviceDriverEntity
    {
        [Key]
        public int Id { get; set; }
        public DateTime? DateStartedUsing { get; set; }
        public bool IsAction { get; set; } = false;
        public bool IsProblem { get; set; } = false;
        public bool IsAuto { get; set; } = false;

        [ForeignKey("DeviceDriverType")]
        public int? DeviceDriverTypeId { get; set; }
        public DeviceDriverTypeEntity? DeviceDriverType { get; set; }

        [ForeignKey("Zone")]
        public int? ZoneId { get; set; }
        public ZoneEntity? Zone { get; set; }
        public ICollection<MachineWarranlyDateEntity>? MachineWarranlyDates { get; set; }
        public ICollection<ImageEntity>? Images { get; set; }
        public ICollection<TimerDeviceDriverEntity>? TimerDevices { get; set; }
        public ICollection<DeviceInstrumentOnOffEntity>? DeviceInstrumentOnOffs { get; set; }
    }
}
