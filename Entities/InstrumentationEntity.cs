using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    public class InstrumentationEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        [ForeignKey("Zone")]
        public int ZoneId { get; set; }
        public ZoneEntity? Zone { get; set; }
        public string? Note { get; set; }
        public string? Description { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? DateOfPurchanse { get; set; }
        public ICollection<InstrumentationEntity>? Instrumentations { get; set; }
    }
}
