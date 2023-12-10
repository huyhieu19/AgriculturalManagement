using System.ComponentModel.DataAnnotations;

namespace Models.DeviceTimer
{
    public class TimerDeviceDriverUpdateModel
    {
        public int Id { get; set; }
        [Required]
        public string NameRef { get; set; } = null!;
        public DateTime? ShutDownTimer { get; set; }
        public DateTime? OpenTimer { get; set; }
        public string? Note { get; set; }
        [Required(ErrorMessage = "Thiếu DeviceDriverId")]
        public Guid DeviceDriverId { get; set; }
    }
}
