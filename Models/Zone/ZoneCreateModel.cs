using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class ZoneCreateModel
    {
        [Required(ErrorMessage = "Please enter ZoneName")]
        public string ZoneName { get; set; } = null!;
        public double? Area { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public string? Note { get; set; }
        public DateTime? HarvestTime { get; set; }
        public DateTime? TimeToStartPlanting { get; set; } = DateTime.UtcNow;
        public string? Function { get; set; }
        public int? TypeTreeId { get; set; }
        [Required(ErrorMessage = "Please enter FarmId")]
        public int FarmId { get; set; }
    }
}