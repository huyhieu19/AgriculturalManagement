using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class ZoneUpdateModel
    {
        [Required(ErrorMessage = "Please enter Id")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter ZoneName")]
        public string ZoneName { get; set; } = null!;
        public double? Area { get; set; }
        public string? Description { get; set; }
        public string? Note { get; set; }
        public DateTime? TimeToStartPlanting { get; set; }
        public DateTime? DateCreateFarm { get; set; }
        public string? Function { get; set; }
        public int? TypeTreeId { get; set; }
        [Required(ErrorMessage = "Please enter FarmId")]
        public int FarmId { get; set; }
    }
}