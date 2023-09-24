using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class ZoneHarvestEntity
    {
        [Key]
        public int Id { get; set; }
        public DateTime HarvertTime { get; set; }
        public double? Quantity { get; set; } // default kg
        public string? Note { get; set; }
        public int? ZoneId { get; set; }
        public ZoneEntity? Zone { get; set; }
        public ICollection<ImageEntity>? Images { get; set; }
    }
}
