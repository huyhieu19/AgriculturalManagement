namespace Models
{
    public class DeviceDriverCreateModel
    {
        public DateTime? DateStartedUsing { get; set; }
        public bool? IsAction { get; set; } = false;
        public bool? IsProblem { get; set; } = false;
        public int? ZoneId { get; set; }
        public int? DeviceDriverTypeId { get; set; }
        public Guid? EspId { get; set; }
        public string? Gpio { get; set; }
    }
}