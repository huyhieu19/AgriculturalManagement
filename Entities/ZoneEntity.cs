using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    public class ZoneEntity
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Note { get; set; }
        [ForeignKey("Farm")]
        public int FarmId { get; set; }
        public FarmEntity? Farm { get; set; }
        public string Function { get; set; } = null!;
        public TypeTreeEntity? Type { get; set; }
        [ForeignKey("Type")]
        public int TypeTreeId { get; set; }
        public DateTime HarvestTime { get; set; }
        public DateTime TimeToStartPlanting { get; set; }
        public ICollection<ZoneDeviceDriverEntity>? ZoneDeviceDrivers { get; set; }
        public ICollection<InstrumentationEntity>? Instrumentations { get; set; }
    }
}