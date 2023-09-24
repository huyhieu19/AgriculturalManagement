using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    public class InstrumentationEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Note { get; set; }
        public string? Description { get; set; }
        public bool? IsActive { get; set; } = false;
        public bool? IsProblem { get; set; } = false;
        public DateTime? DateOfPurchanse { get; set; }

        [ForeignKey("Zone")]
        public int? ZoneId { get; set; }

        public ZoneEntity? Zone { get; set; }

        public ICollection<MachineWarranlyDateEntity>? MachineWarranlyDates { get; set; }
        public ICollection<ImageEntity>? Images { get; set; }
    }
}
