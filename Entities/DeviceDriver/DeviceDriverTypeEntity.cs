using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class DeviceDriverTypeEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Manufacturer { get; set; }
        public string? ImageUrl { get; set; }
        public ICollection<DeviceDriverEntity>? DeviceDrivers { get; set; }

        public DeviceDriverTypeEntity(int Id, string name, string? description = null, string? manufacturer = null, string? imageUrl = null)
        {
            this.Id = Id;
            Name = name;
            Description = description;
            Manufacturer = manufacturer;
            ImageUrl = imageUrl;
        }
    }
}
