﻿using NLog;
using Service.Contracts;

namespace Service
{
    public class LoggerManager : ILogerManager
    {
        private static ILogger logger = LogManager.GetCurrentClassLogger();

        public LoggerManager()
        {
        }

        public void LogDebug(string message) => logger.Debug(message);

        public void LogError(string message) => logger.Error(message);

        public void LogInfomation(string message) => logger.Info(message);

        public void LogWarning(string message) => logger.Warn(message);
    }
}