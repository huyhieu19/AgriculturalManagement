using Microsoft.AspNetCore.Http;

namespace Models
{
    public class ImageCreateModel
    {
        public IFormFile FileImage { get; set; } = null!;
        public bool? IsDefault { get; set; }
        public string? Name { get; set; }

        public int? FarmId { get; set; }
        public int? StaffId { get; set; }
        public string? UserId { get; set; }
        public int? ZoneId { get; set; }
        public int? ZoneHarvestId { get; set; }
    }
}
