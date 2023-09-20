using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class TypeTreeEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? Note { get; set; }
        public ICollection<ZoneEntity>? Zones { get; set; }
    }
}
