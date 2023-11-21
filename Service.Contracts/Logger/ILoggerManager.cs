using Common.Enum;
using Entities;

namespace Service.Contracts.Logger
{
    public interface ILoggerManager
    {
        void LogInformation(string message, string? ServiceName = null, LoggerProcessType? ProcessType = LoggerProcessType.None, string? MessageDetail = null, string? User = null);
        void LogWarning(string message, string? ServiceName = null, LoggerProcessType? ProcessType = LoggerProcessType.None, string? MessageDetail = null, string? User = null);
        void LogDebug(string message, LogProcessModel? logProcessModel = null);
        void LogError(string message, string? ServiceName = null, LoggerProcessType? ProcessType = LoggerProcessType.None, string? MessageDetail = null, string? User = null);
    }
}