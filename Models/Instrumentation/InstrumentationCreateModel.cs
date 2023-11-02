using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class InstrumentationCreateModel
    {
        public string Name { get; set; } = null!;
        public string? Note { get; set; }
        public string? Description { get; set; }
        [Required]
        public bool? IsActive { get; set; } = false;
        public DateTime? DateStartedUsing { get; set; }
        public int? ZoneId { get; set; }
        public int? InstrumentationTypeId { get; set; }
        public Guid? EspId { get; set; }
        public string? Gpio { get; set; }
    }
}