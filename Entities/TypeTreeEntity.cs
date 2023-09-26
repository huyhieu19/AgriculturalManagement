using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class TypeTreeEntity
    {
        [Key]
        public int Id { get; set; }
        public string NameType { get; set; }
        public string? Description { get; set; }
        public string? Note { get; set; }
        public string? ImageUrl { get; set; }
        public ICollection<ZoneEntity>? Zones { get; set; }
        public TypeTreeEntity(int Id, string NameType, string? Description = null)
        {
            this.Id = Id;
            this.NameType = NameType;
            this.Description = Description;
        }
    }
}
