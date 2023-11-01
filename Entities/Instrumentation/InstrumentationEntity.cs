using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    public class InstrumentationEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public bool? IsActive { get; set; } = false;
        public DateTime? DateStartedUsing { get; set; }

        [ForeignKey("Zone")]
        public int? ZoneId { get; set; }

        public ZoneEntity? Zone { get; set; }
        [ForeignKey("InstrumentationType")]
        public Guid? InstrumentationTypeId { get; set; }

        public Guid? EspId { get; set; }
        public Esp8266Entity? Esp8266 { get; set; }

        public string? Gpio { get; set; }

        public string Topic
        {
            get;
            set;
        }

        public InstrumentationTypeEntity? InstrumentationType { get; set; }
        public ICollection<MachineWarranlyDateEntity>? MachineWarranlyDates { get; set; }
        public ICollection<DeviceInstrumentThresholdEntity>? DeviceInstrumentOnOffs { get; set; }
    }
}
