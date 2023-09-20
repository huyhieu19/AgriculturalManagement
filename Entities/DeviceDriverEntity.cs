using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class DeviceDriverEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public int ZoneId { get; set; }
        public bool IsActive { get; set; } = false;
        public ICollection<MachineWarranlyDateEntity>? MachineWarranlyDates { get; set; }
        public ICollection<ZoneDeviceDriverEntity>? ZoneDeviceDrivers { get; set; }
    }
}
