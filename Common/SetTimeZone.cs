using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class SetTimeZone
    {
        private static readonly DateTime TimeZone = DateTime.Now.AddHours(7);
        public static DateTime GetTimeZone() { return TimeZone;}
    }
}
