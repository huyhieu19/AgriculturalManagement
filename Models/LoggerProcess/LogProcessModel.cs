using Common.Enum;

namespace Models
{
    public class LogProcessModel
    {
        public string? ServiceName { get; set; }
        public string? LogMessageDetail { get; set; }
        public LoggerProcessType LoggerProcessType { get; set; }
        public string? User { get; set; }
    }
}
