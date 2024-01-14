using System.ComponentModel;

namespace Common.Enum
{
    public enum FunctionDeviceType
    {
        [Description("ONOFF")]
        None = 0,
        [Description("AirTemperature")]
        AirTemperature = 1,
        [Description("AirHumidity")]
        AirHumidity = 2,
        [Description("SoilMoisture")]
        SoilMoisture = 3,
        [Description("RainDetection")]
        RainDetection = 4,
    }
}
