using Common.Enum;
using Microsoft.Extensions.Hosting;
using Models;
using MQTTProcess;
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
            while (true)
            {
                try
                {
                    logger.LogInformation("Start threshold");
                    await deviceJobInstrumentation.RunningJobThreshold();
                    await Task.Delay(TimeSpan.FromSeconds(10));
                    logger.LogInformation("End threshold");
                }
                catch (Exception ex)
                {
                    logger.LogError(ex.Message, new LogProcessModel()
                    {
                        LoggerProcessType = LoggerProcessType.ThresholdDevice,
                        LogMessageDetail = ex.ToString(),
                        ServiceName = $"{nameof(ProcessJobMqtt)} -> {nameof(ExecuteAsync)}",
                        User = "Auto"
                    });
                    await Task.Delay(TimeSpan.FromSeconds(5));
                    throw;
                }
            }
        }
    }
}
