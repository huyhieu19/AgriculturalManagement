using Microsoft.Extensions.Hosting;
using Models;
using Service.Contracts;


namespace Service.BackgroundJob
{
    public class JobThresholdService : BackgroundService
    {
        private readonly ILoggerManager logger;
        private readonly IDeviceAutoService deviceAutoService;

        public JobThresholdService(ILoggerManager logger,
            IDeviceAutoService deviceAutoService
            )
        {
            this.logger = logger;
            this.deviceAutoService = deviceAutoService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // Run every 5 seconds
            while (!stoppingToken.IsCancellationRequested)
            {
                logger.LogInfomation("Start JobThresholdService");
                await AutoOnOffAccordingToThreshold();
                logger.LogInfomation("End JobThresholdService");
                await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
            }
        }

        private int IsTurnOnDevice(double randomValue, InstrumentSetThresholdDisplayModel instrumentation)
        {
            bool upperThreshold = instrumentation.OnInUpperThreshold;

            if ((upperThreshold && randomValue > instrumentation.ThresholdValueOn) ||
                (!upperThreshold && randomValue < instrumentation.ThresholdValueOff))
            {
                return 1;
            }

            if ((!upperThreshold && randomValue > instrumentation.ThresholdValueOff) ||
                (upperThreshold && randomValue < instrumentation.ThresholdValueOn))
            {
                return 2;
            }

            return 0;
        }


        private async Task AutoOnOffAccordingToThreshold()
        {
            var AccordingToThresholds = await deviceAutoService.DeviceInstrumentOnOffNotDelete();
            foreach (var item in AccordingToThresholds)
            {
                var randomValue = 30;
                int check = IsTurnOnDevice(randomValue, item);
                if (check == 1)
                {
                    await deviceAutoService.DeviceDriverTurnOn(item.DeviceDriverId);
                }
                else if (check == 2)
                {
                    await deviceAutoService.DeviceDriverTurnOff(item.DeviceDriverId);
                }
            }
        }
    }
}
