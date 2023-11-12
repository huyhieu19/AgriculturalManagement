// Ignore Spelling: Gpio

namespace Common.Enum
{
    public enum ESP8266GateTransferDataType
    {
        D0 = 0,// GPIO16
        D1 = 1,// GPIO5
        D2 = 2,// GPIO4
        D3 = 3,// GPIO0
        D4 = 4,// GPIO2
        D5 = 5,// GPIO14
        D6 = 6,// GPIO12
        D7 = 7,// GPIO13
        D8 = 8,// GPIO15
    }

    public enum ESP32GateTransferDataType
    {
        D0 = 0,// GPIO16
        D1 = 1,// GPIO5
        D2 = 2,// GPIO4
        D3 = 3,// GPIO0
        D4 = 4,// GPIO2
        D5 = 5,// GPIO14
        D6 = 6,// GPIO12
        D7 = 7,// GPIO13
        D8 = 8,// GPIO15
    }

    public enum GpioGateNotTransferDataType
    {
        CLK = 9,// GPIO6
        SDO = 10,// GPIO7
        CMD = 11,// GPIO11
        SD1 = 12,// GPIO8
        SD2 = 13,// GPIO9
        SD3 = 14,// GPIO10
    }
}
