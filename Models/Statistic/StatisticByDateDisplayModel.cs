using Common.Enum;

namespace Models.Statistic
{
    public class StatisticByDateDisplayModel
    {
        public string NameDevice { get; set; } = string.Empty;
        public StatisticType Type { get; set; }
        public double? ValueAVG { get; set; }
        public double? ValueMAX { get; set; }
        public double? ValueMIN { get; set; }
        List<double> Values { get; set; } = new List<double>();
        public int? CountOn { get; set; }
        public int? CountOff { get; set; }
        public int CountTotal { get; set; }
        public DateTime ValueDate { get; set; }
    }
}
