using Common.Enum;
using Microsoft.Extensions.Hosting;
using Models;
using MQTTProcess;
using Service.Contracts;
using Service.Contracts.Logger;

namespace JobBackground.DeviceAuto
{
    public class AsyncStatusDeviceService : BackgroundService
    {
        private readonly IDeviceControlService deviceControlService;
        private readonly ILoggerManager logger;
        public AsyncStatusDeviceService(IDeviceControlService deviceControlService, ILoggerManager logger)
        {
            this.deviceControlService = deviceControlService;
            this.logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (true)
            {
                try
                {
                    logger.LogInformation("Start Async");
                    await deviceControlService.AsyncStatusDeviceControl(); // Simulate work.
                    await Task.Delay(TimeSpan.FromSeconds(30));
                    logger.LogInformation("End Async");
                }
                catch (Exception ex)
                {
                    // thưc hiện ghi log
                    logger.LogError(ex.Message, new LogProcessModel()
                    {
                        LoggerProcessType = LoggerProcessType.AsyncStatusDevice,
                        LogMessageDetail = ex.ToString(),
                        ServiceName = $"{nameof(ProcessJobMqtt)} -> {nameof(ExecuteAsync)}",
                        User = "Auto"
                    });
                    await Task.Delay(TimeSpan.FromSeconds(30));
                    throw;
                }
            }
        }
    }
}
