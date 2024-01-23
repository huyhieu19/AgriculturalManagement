using Common.Enum;

namespace Models.DeviceData
{
    public class DeviceDataQueryModel : BaseQueryModel
    {
        public DateTime? ValueDate { get; set; }
    }

    public class LogDeviceDataQueryModel : BaseQueryModel
    {
        public DateTime? ValueDate { get; set; }
        public TypeOnOff? TypeOnOff { get; set; } = null;
        public int? ThresholdId { get; set; }
    }
}
