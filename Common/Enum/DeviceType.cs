using System.ComponentModel;

namespace Common.Enum
{
    public enum DeviceType
    {
        None = 0,
        [Description("W")]
        W = 1, // DeviceDriver
        [Description("R")]
        R = 2 // Instrumentation
    }
}
