using Entities.User;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    public class FarmEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? Address { get; set; }
        public string? Note { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.Now;


        [ForeignKey("User")]
        public string? UserId { get; set; }


        public UserEntity? User { get; set; }


        public ICollection<ImageEntity>? Images { get; set; }
        public ICollection<ZoneEntity>? Zones { get; set; }
    }
}
