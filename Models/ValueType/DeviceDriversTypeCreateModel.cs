namespace Models
{
    public class DeviceDriversTypeCreateModel
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? Manufacturer { get; set; }
        public string? ImageUrl { get; set; }
    }
}