using Microsoft.Extensions.Hosting;
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
            while (!stoppingToken.IsCancellationRequested)
            {
                logger.LogInformation("Start Async");
                await deviceControlService.AsyncStatusDeviceControl(); // Simulate work.
                await Task.Delay(TimeSpan.FromSeconds(30));
                logger.LogInformation("End Async");
            }
        }
    }
}
