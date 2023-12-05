using Common.Enum;

namespace Models.DeviceControl
{
    public class OnOffDeviceQueryModel
    {
        public Guid ModuleId { get; set; }
        public Guid DeviceId { get; set; }
        public DeviceType DeviceType { get; set; }
        public string DeviceNameNumber { get; set; } = null!;
        public bool RequestOn { get; set; }
    }
}
