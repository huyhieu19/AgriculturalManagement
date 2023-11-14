namespace Models.DeviceTimer
{
    public class TimerDeviceDriverDisplayModel
    {
        public int Id { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public string? Note { get; set; }

        public DateTime? ShutDownTimer { get; set; }
        public DateTime? OpenTimer { get; set; }

        public bool? IsAffected { get; set; }
        public bool IsSuccess { get; set; }
        public bool IsRemove { get; set; }

        public Guid DeviceId { get; set; }
    }
}