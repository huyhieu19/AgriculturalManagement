using Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Statistic
{
    public class StatisticDisplayModel
    {
        public string NameDevice { get; set; } = string.Empty;
        public StatisticType Type { get; set; }
        public double? ValueAVG { get; set; }
        public double? ValueMAX { get; set; }
        public double? ValueMIN { get; set; }
        public int? CountOn { get; set; }
        public int? CountOff { get; set; }
        public int Count { get; set; }
        public DateTime ValueDate { get; set; }
    }
}
