using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.Farm;

namespace Entities.Image
{
    public class ImageEntity
    {
        [Key]
        public int Id { get; set; }
        public string Url { get; set; } = null!;
        public bool? IsDefault { get; set; } = false;
        public string? Name { get; set; }


        [ForeignKey("Farms")]
        public int? FarmId { get; set; }
        [ForeignKey("Zone")]
        public int? ZoneId { get; set; }
        [ForeignKey("ZoneHarvest")]
        public int? ZoneHarvestId { get; set; }
        [ForeignKey("Instrumentation")]
        public int? InstrumentationId { get; set; }
        [ForeignKey("DeviceDriver")]
        public int? DeviceDriverId { get; set; }

        public ZoneEntity? Zone { get; set; }
        public FarmEntity? Farm { get; set; }
        public ZoneHarvestEntity? ZoneHarvest { get; set; }
    }
}