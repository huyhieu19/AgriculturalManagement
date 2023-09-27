namespace Models.DeviceDriver
{
    public class DeviceDriverDisplayModel
    {
        public int Id { get; set; }
        public bool? IsDaily { get; set; } = false;
        public bool? IsAuto { get; set; } = false;
        public int? ShutDownTime { get; set; }
        public int? OpenTimer { get; set; }
        public DateTime? DateStartedUsing { get; set; }
        public bool? IsAction { get; set; } = false;
        public bool? IsProblem { get; set; } = false;
        public int? ZoneId { get; set; }
        public string? ZoneName { get; set; }
        public string? DescriptionZone { get; set; }
        public int? DeviceDriverTypeId { get; set; }
        public string? DeviceDriverTypeName { get; set; }
        public string? DeviceDriverTypeDescription { get; set; }
        public string? DeviceDriverTypeManufacturer { get; set; }
        public string? DeviceDriverTypeImageUrl { get; set; }
    }
}
