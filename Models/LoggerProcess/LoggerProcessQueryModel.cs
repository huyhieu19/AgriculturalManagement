using Common.Enum;

namespace Models.LoggerProcess
{
    public class LoggerProcessQueryModel : BaseQueryModel
    {
        public LoggerProcessType? LoggerProcessType { get; set; }
        public LoggerType? LoggerType { get; set; }
        public DateTime? ValueDate { get; set; }
    }
}
