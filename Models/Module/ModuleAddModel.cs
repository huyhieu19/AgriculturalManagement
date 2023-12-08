using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class ModuleAddModel
    {
        [Required]
        public Guid ModuleId { get; set; }
        [Required]
        public string? NameRef { get; set; }
    }
}
