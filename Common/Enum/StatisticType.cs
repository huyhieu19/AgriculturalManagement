using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Enum
{
    public enum StatisticType
    {
        None = 0,
        ValueDouble = 1, // độ ẩm nhiệt độ,% đô ẩm đát
        ValueOnOff = 2, // như thiết bi điều khiển ( máy bơm, quạt ... số lần)
        ValueDetect = 3,//như phát hiện mưa...
    }
}
