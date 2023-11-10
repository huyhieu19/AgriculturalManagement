using Common;
using Common.Enum;

namespace Models
{
    public class DeviceDriverCreateModel
    {
        public DateTime? DateStartedUsing { get; set; } = SetTimeZone.GetDateTimeVN();
        public string? Name { get; set; }
        public bool? IsAction { get; set; } = false;
        public int ZoneId { get; set; }
        public Guid EspId { get; set; }
        public Guid? DeviceTypeId { get; set; }
        public GpioGateType Gpio { get; set; }
    }
}