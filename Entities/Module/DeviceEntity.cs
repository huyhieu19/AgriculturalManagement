// Ignore Spelling: Gpio

using Common.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Module
{
    public class DeviceEntity
    {
        // PK
        public Guid Id { get; set; }
        // FK
        public Guid ModuleId { get; set; }
        [ForeignKey("Zone")]
        public int? ZoneId { get; set; }
        public string? Name { get; set; }
        public string? NameRef { get; set; }
        public string? Description { get; set; }
        public DateTime? DateCreated { get; set; }
        public bool IsAction { get; set; }
        public bool IsUsed { get; set; }
        public bool IsAuto { get; set; }
        public string? Unit { get; set; }
        public string? Gate { get; set; }
        public DeviceType DeviceType { get; set; }
        public ZoneEntity? Zone { get; set; }
        public ModuleEntity? Module { get; set; }
        public ICollection<TimerDeviceDriverEntity>? TimerDevices { get; set; }
        public ICollection<DeviceInstrumentThresholdEntity>? DeviceInstrumentOnOffs { get; set; }
    }
}
