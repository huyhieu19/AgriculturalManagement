using System.ComponentModel.DataAnnotations;

namespace Models.DeviceTimer
{
    public class TimerDeviceDriverCreateModel
    {
        public DateTime? ShutDownTimer { get; set; }
        public DateTime? OpenTimer { get; set; }
        public string? Note { get; set; }

        [Required(ErrorMessage = "Thiếu DeviceDriverId")]
        public Guid DeviceDriverId { get; set; }
    }
}