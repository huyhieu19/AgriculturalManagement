using Common;
using Microsoft.Extensions.Hosting;
using Service.Contracts;

namespace Service.BackgroundJob
{
    public class JobForDeviceDriverService : BackgroundService
    {
        private readonly ILoggerManager logger;
        private readonly IDeviceAutoService deviceAutoService;
        private readonly DateTime timeZoneNow = SetTimeZone.GetTimeZone();

        public JobForDeviceDriverService(ILoggerManager logger, IDeviceAutoService deviceAutoService)
        {
            this.logger = logger;
            this.deviceAutoService = deviceAutoService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // Run every 5 seconds
            while (!stoppingToken.IsCancellationRequested)
            {
                logger.LogInfomation("Start job device driver on off according to timer");
                await ToDoAsyncIsAuto();
                logger.LogInfomation("End job device driver on off according to timer");
                await Task.Delay(TimeSpan.FromSeconds(7), stoppingToken);
            }
        }



        private async Task ToDoAsyncIsAuto()
        {
            var listTime = await deviceAutoService.GetDeviceDriverTurnOnTurnOffModels();

            if (listTime != null && listTime.Any(p => p!.OpenTimer!.Value.Minute == timeZoneNow.Minute && p.IsAuto))
            {
                var entities = listTime.Where(p => p!.OpenTimer!.Value.Minute == timeZoneNow.Minute)!.ToList();

                foreach (var entity in entities)
                {
                    int? Id = entity!.DeviceDriverId;
                    await deviceAutoService.DeviceDriverTurnOn((int)Id);
                }
                await Task.Delay(TimeSpan.FromMinutes(30));
            }


            if (listTime != null && listTime.Any(p => p!.ShutDownTimer!.Value.Minute == timeZoneNow.Minute && p.IsAuto))
            {
                var entities = listTime.Where(p => p!.ShutDownTimer!.Value.Minute == timeZoneNow.Minute)!.ToList();

                foreach (var entity in entities)
                {
                    int? Id = entity!.DeviceDriverId;
                    await deviceAutoService.DeviceDriverTurnOff((int)Id);
                    await deviceAutoService.DeleteTimer((int)Id);
                }
                await Task.Delay(TimeSpan.FromMinutes(30));
            }
        }
    }
}
