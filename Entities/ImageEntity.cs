using Entities.User;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    public class ImageEntity
    {
        [Key]
        public int Id { get; set; }
        public string Url { get; set; } = null!;
        public bool? IsDefault { get; set; } = false;
        public string? Name { get; set; }


        [ForeignKey("Farm")]
        public int? FarmId { get; set; }
        [ForeignKey("Staff")]
        public int? StaffId { get; set; }
        [ForeignKey("User")]
        public string? UserId { get; set; }
        [ForeignKey("Zone")]
        public int? ZoneId { get; set; }
        [ForeignKey("ZoneHarvest")]
        public int? ZoneHarvestId { get; set; }
        [ForeignKey("Instrumentation")]
        public int? InstrumentationId { get; set; }
        [ForeignKey("DeviceDriver")]
        public int? DeviceDriverId { get; set; }
        [ForeignKey("Machine")]
        public int? MachineId { get; set; }

        public ZoneEntity? Zone { get; set; }
        public UserEntity? User { get; set; }
        public StaffEntity? Staff { get; set; }
        public FarmEntity? Farm { get; set; }
        public ZoneHarvestEntity? ZoneHarvest { get; set; }
        public InstrumentationEntity? Instrumentation { get; set; }
        public DeviceDriverEntity? DeviceDriver { get; set; }
        public MachineEntity? Machine { get; set; }
    }
}
