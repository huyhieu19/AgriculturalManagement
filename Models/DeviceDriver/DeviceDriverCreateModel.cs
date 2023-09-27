namespace Models.DeviceDriver
{
    public class DeviceDriverCreateModel
    {
        public bool? IsDaily { get; set; } = false;
        public bool? IsAuto { get; set; } = false;
        public int? ShutDownTime { get; set; }
        public int? OpenTimer { get; set; }
        public DateTime? DateStartedUsing { get; set; }
        public bool? IsAction { get; set; } = false;
        public bool? IsProblem { get; set; } = false;
        public int? ZoneId { get; set; }
        public int? DeviceDriverTypeId { get; set; }
    }
}
