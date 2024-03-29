﻿using Entities.LogProcess;
using Models;

namespace Service.Contracts.Logger
{
    public interface ILoggerManager
    {
        void LogInformation(string message, LogProcessModel? logProcessModel = null);
        void LogWarning(string message, LogProcessModel? logProcessModel = null);
        void LogDebug(string message, LogProcessModel? logProcessModel = null);
        void LogError(string message, LogProcessModel? logProcessModel = null);
        Task LogOnOffDevice(LogDeviceStatusEntity model);
        Task LogMultipleOnOffDevice(List<LogDeviceStatusEntity> model);
    }
}