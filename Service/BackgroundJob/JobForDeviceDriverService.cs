﻿//using Common.TimeHelper;
//using Microsoft.Extensions.Hosting;
//using Service.Contracts;
//using Service.Contracts.Logger;

//namespace Service.BackgroundJob
//{
//    public class JobForDeviceDriverService : BackgroundService
//    {
//        private readonly ILoggerManager logger;
//        private readonly IDevicControlService deviceAutoService;
//        private readonly DateTime timeZoneNow = SetTimeZone.GetTimeZone();

//        public JobForDeviceDriverService(ILoggerManager logger, IDevicControlService deviceAutoService)
//        {
//            this.logger = logger;
//            this.deviceAutoService = deviceAutoService;
//        }

//        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
//        {
//            // Run every 5 seconds
//            while (!stoppingToken.IsCancellationRequested)
//            {
//                logger.LogInformation("Start job device driver on off according to timer");
//                await ToDoAsyncIsAuto();
//                logger.LogInformation("End job device driver on off according to timer");
//                await Task.Delay(TimeSpan.FromSeconds(50), stoppingToken);
//            }
//        }



//        private async Task ToDoAsyncIsAuto()
//        {
//            var listTime = await deviceAutoService.GetDeviceDriverTurnOnTurnOffModels();

//            if (listTime != null && listTime.Any(p => p!.OpenTimer!.Value.Minute == timeZoneNow.Minute && p.IsAuto))
//            {
//                var entities = listTime.Where(p => p!.OpenTimer!.Value.Minute == timeZoneNow.Minute)!.ToList();

//                foreach (var entity in entities)
//                {
//                    int Id = entity!.DeviceDriverId;
//                    await deviceAutoService.DeviceDriverTurnOn(Id);
//                }
//            }
//            await Task.Delay(TimeSpan.FromSeconds(5));

//            if (listTime != null && listTime.Any(p => p!.ShutDownTimer!.Value.Minute == timeZoneNow.Minute && p.IsAuto))
//            {
//                var entities = listTime.Where(p => p!.ShutDownTimer!.Value.Minute == timeZoneNow.Minute)!.ToList();

//                foreach (var entity in entities)
//                {
//                    int Id = entity!.DeviceDriverId;
//                    await deviceAutoService.DeviceDriverTurnOff(Id);
//                    await deviceAutoService.DeleteTimer(Id);
//                }
//            }
//            await Task.Delay(TimeSpan.FromSeconds(5));
//        }
//    }
//}