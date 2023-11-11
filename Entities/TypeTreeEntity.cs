using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class TypeTreeEntity
    {
        [Key]
        public int Id { get; set; }
        public string NameType { get; set; } = null;
        public string? Description { get; set; }
        public string? Note { get; set; }
        public string? ImageUrl { get; set; }
        public ICollection<ZoneEntity>? Zones { get; set; }

    }
}