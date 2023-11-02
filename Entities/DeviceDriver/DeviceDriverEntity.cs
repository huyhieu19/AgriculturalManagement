using Entities.ESP;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    public class DeviceDriverEntity
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime? DateStartedUsing { get; set; }
        public bool IsAction { get; set; } = false;
        public bool IsAuto { get; set; } = false;

        public Guid? DeviceTypeId { get; set; }
        public DeviceTypeOnEspEntity? DeviceType { get; set; }

        [ForeignKey("Zone")]
        public int? ZoneId { get; set; }
        public ZoneEntity? Zone { get; set; }

        public Guid? EspId { get; set; }
        public EspEntity? Esp { get; set; }

        public string? Gpio { get; set; }

        public string? Topic { get; set; }

        public ICollection<TimerDeviceDriverEntity>? TimerDevices { get; set; }
        public ICollection<DeviceInstrumentThresholdEntity>? DeviceInstrumentOnOffs { get; set; }
    }
}