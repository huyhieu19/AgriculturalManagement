using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class DeviceDriverTypeEntity
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? Manufacturer { get; set; }
        public ICollection<DeviceDriverEntity>? DeviceDrivers { get; set; }

    }
}