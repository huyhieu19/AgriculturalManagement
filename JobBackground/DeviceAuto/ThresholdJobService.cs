using Microsoft.Extensions.Hosting;
using Service.Contracts.DeviceThreshold;
using Service.Contracts.Logger;

namespace JobBackground.DeviceAuto
{
    public class ThresholdJobService : BackgroundService
    {
        private readonly IDeviceJobInstrumentationService deviceJobInstrumentation;
        private readonly ILoggerManager logger;

        public ThresholdJobService(IDeviceJobInstrumentationService deviceJobInstrumentation, ILoggerManager logger)
        {
            this.deviceJobInstrumentation = deviceJobInstrumentation;
            this.logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                logger.LogInformation("Start threshold");
                await deviceJobInstrumentation.RunningJobThreshold();
                await Task.Delay(TimeSpan.FromSeconds(10));
                logger.LogInformation("End threshold");
            }
        }
    }
}
