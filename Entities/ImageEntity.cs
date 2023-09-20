using Entities.User;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    public class ImageEntity
    {
        [Key]
        public int Id { get; set; }
        public string Url { get; set; } = null!;
        public string? Name { get; set; }
        [ForeignKey("Farm")]
        public int FarmId { get; set; }
        [ForeignKey("Staff")]
        public int StaffId { get; set; }
        [ForeignKey("User")]
        public string? UserId { get; set; }
        [NotMapped]
        public UserEntity? User { get; set; }
        public StaffEntity? Staff { get; set; }
        public FarmEntity? Farm { get; set; }
    }
}
