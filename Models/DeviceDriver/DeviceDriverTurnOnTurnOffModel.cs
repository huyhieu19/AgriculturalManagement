namespace Models
{
    public class DeviceDriverTurnOnTurnOffModel
    {
        public int Id { get; set; }
        public bool? IsDaily { get; set; } = false;
        public bool IsAuto { get; set; } = false;
        public DateTime ShutDownTimer { get; set; }
        public DateTime OpenTimer { get; set; }
        public int DeviceDriverId { get; set; }
    }
}
