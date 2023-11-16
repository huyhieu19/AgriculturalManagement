using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.GpioGate
{
    public static class GpioGate
    {
        public readonly static List<string> ESP8266Gates = new List<string>
        {
            "D0",// GPIO16
            "D1",// GPIO5
            "D2",// GPIO4
            "D3",// GPIO0
            "D4",// GPIO2
            "D5",// GPIO14
            "D6",// GPIO12
            "D7",// GPIO13
            "D8",// GPIO15
            "CLK",// GPIO6
            "SDO",// GPI7
            "CMD",// GPIO11
            "SD1",// GPIO8
            "SD2",// GPIO9
            "SD3",// GPIO10
        };
    }
}
