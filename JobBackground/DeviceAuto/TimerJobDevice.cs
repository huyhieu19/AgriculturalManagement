using Microsoft.Extensions.Hosting;
using Models.DeviceControl;
using Service.Contracts;
using Service.Contracts.Logger;

namespace JobBackground.DeviceAuto
{
    public class TimerJobDevice : BackgroundService
    {
        private readonly ILoggerManager logger;
        private readonly IDeviceControlService deviceControlService;

        public TimerJobDevice(ILoggerManager logger, IDeviceControlService deviceControlService)
        {
            this.logger = logger;
            this.deviceControlService = deviceControlService;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // Run every 5 seconds
            while (!stoppingToken.IsCancellationRequested)
            {
                logger.LogInformation("Start Timer Job service");
                await ToDoAsyncIsAuto(); // Simulate work.
                logger.LogInformation("End Timer Job service");
                await Task.Delay(TimeSpan.FromSeconds(10));
            }
        }

        private async Task ToDoAsyncIsAuto()
        {
            var listTime = await deviceControlService.GetAllTimerAvailable();

            if (listTime.Any(p => p!.OpenTimer!.Value.Minute == DateTime.Now.AddHours(+7).Minute && p.IsAuto) || listTime.Any(p => p!.ShutDownTimer!.Value.Minute == DateTime.Now.AddHours(+7).Minute))
            {
                var entitiesTurnOn = listTime.Where(p => p!.OpenTimer!.Value.Minute == DateTime.Now.AddHours(+7).Minute && !p.IsSuccessON)!.ToList();
                var entitiesTurnOff = listTime.Where(p => p!.ShutDownTimer!.Value.Minute == DateTime.Now.AddHours(+7).Minute && !p.IsSuccessOFF)!.ToList();

                foreach (var entity in entitiesTurnOn)
                {
                    var model = new OnOffDeviceQueryModel()
                    {
                        DeviceId = entity.DeviceId,
                        DeviceType = entity.DeviceType,
                        DeviceNameNumber = entity.NameRef,
                        RequestOn = true,
                    };
                    var IsComplete = await deviceControlService.DeviceDriverOnOff(model);
                    if (IsComplete)
                    {
                        await deviceControlService.SuccessJobTurnOnDeviceTimer(entity.Id, entity.DeviceId);
                    }
                }
                foreach (var entity in entitiesTurnOff)
                {
                    var model = new OnOffDeviceQueryModel()
                    {
                        DeviceId = entity.DeviceId,
                        DeviceType = entity.DeviceType,
                        DeviceNameNumber = entity.NameRef,
                        RequestOn = false,
                    };
                    var IsComplete = await deviceControlService.DeviceDriverOnOff(model);
                    if (IsComplete)
                    {
                        await deviceControlService.SuccessJobTurnOffDeviceTimer(entity.Id, entity.DeviceId);
                    }
                }
            }

        }
    }
}
