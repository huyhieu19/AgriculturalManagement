﻿namespace Service.Contracts
{
    public interface ILoggerManager
    {
        void LogInformation(string message);
        void LogWarning(string message);
        void LogDebug(string message);
        void LogError(string message);
    }
}