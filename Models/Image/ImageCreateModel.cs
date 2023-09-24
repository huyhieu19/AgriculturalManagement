using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class ImageCreateModel
    {
        public IFormFile FileImage { get; set; } = null!;
        [Required]
        public bool? IsDefault { get; set; } = false;
        public string? Name { get; set; }

        public int? FarmId { get; set; }
        public int? StaffId { get; set; }
        public string? UserId { get; set; }
        public int? ZoneId { get; set; }
        public int? ZoneHarvestId { get; set; }
        public int? InstrumentationId { get; set; }
        public int? DeviceDriverId { get; set; }
        public int? MachineId { get; set; }
    }
}
