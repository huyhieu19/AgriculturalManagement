namespace Models
{
    public class ImageQueryDisplayModel
    {
        public int? FarmId { get; set; }
        public int? ZoneId { get; set; }
        public string? UserId { get; set; }
        public int? ZoneHarvestId { get; set; }
        public int? InstrumentationId { get; set; }
        public int? DeviceDriverId { get; set; }
        public int? MachineId { get; set; }
        public bool? IsDefault { get; set; }
    }
}
