using Common.Enum;
using System.Diagnostics.Eventing.Reader;

namespace Models.DeviceData
{
    public class DeviceDataQueryModel : BaseQueryModel
    {
        public DateTime? ValueDate { get; set; }
    }

    public class LogDeviceDataQueryModel : BaseQueryModel
    {
        public DateTime? ValueDate { get; set; }
        public TypeOnOff TypeOnOff { get; set; }
    }
}
