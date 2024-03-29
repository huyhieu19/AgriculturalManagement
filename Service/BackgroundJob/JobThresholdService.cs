﻿//using Microsoft.Extensions.Hosting;
//using Models;
//using Service.Contracts;
//using Service.Contracts.Logger;

//namespace Service.BackgroundJob
//{
//    public class JobThresholdService : BackgroundService
//    {
//        private readonly ILoggerManager logger;
//        private readonly IDeviceControlService deviceAutoService;

//        public JobThresholdService(ILoggerManager logger,
//            IDeviceControlService deviceAutoService)
//        {
//            this.logger = logger;
//            this.deviceAutoService = deviceAutoService;
//        }

//        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
//        {
//            // Run every 5 seconds
//            while (!stoppingToken.IsCancellationRequested)
//            {
//                logger.LogInformation("Start JobThresholdService");
//                //await AutoOnOffAccordingToThreshold(); // Simulate work.

//                logger.LogInformation("End JobThresholdService");
//                await Task.Delay(TimeSpan.FromMinutes(2), stoppingToken);
//            }
//        }

//        private int IsTurnOnDevice(double randomValue, InstrumentSetThresholdDisplayModel instrumentation)
//        {
//            bool upperThreshold = instrumentation.OnInUpperThreshold;

//            if ((upperThreshold && randomValue > instrumentation.ThresholdValueOn) ||
//                (!upperThreshold && randomValue < instrumentation.ThresholdValueOff))
//            {
//                return 1;
//            }

//            if ((!upperThreshold && randomValue > instrumentation.ThresholdValueOff) ||
//                (upperThreshold && randomValue < instrumentation.ThresholdValueOn))
//            {
//                return 2;
//            }

//            return 0;
//        }


//        private async Task AutoOnOffAccordingToThreshold()
//        {
//            //var AccordingToThresholds = await deviceAutoService.DeviceInstrumentOnOffNotDelete();
//            //foreach (var item in AccordingToThresholds)
//            //{
//            //    var randomValue = 30;
//            //    int check = IsTurnOnDevice(randomValue, item);
//            //    if (check == 1)
//            //    {
//            //        await deviceAutoService.DeviceDriverTurnOn(item.DeviceDriverId); // Simulate work.

//            //    }
//            //    else if (check == 2)
//            //    {
//            //        await deviceAutoService.DeviceDriverTurnOff(item.DeviceDriverId); // Simulate work.
//            //    }
//            //}
//        }
//    }
//}