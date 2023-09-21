using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class MachineEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? Note { get; set; }
        public DateTime? DateOfPurChanse { get; set; }
        public bool IsActive { get; set; } = false;

        public ICollection<InstrumentationEntity>? Instrumentations { get; set; }
        public ICollection<MachineWarranlyDateEntity>? MachineWarranlyDates { get; set; }
    }
}
