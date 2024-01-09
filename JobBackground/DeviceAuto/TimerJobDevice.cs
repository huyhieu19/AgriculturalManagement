using Common.Enum;
using Entities.LogProcess;
using Microsoft.Extensions.Hosting;
using Models;
using Models.DeviceControl;
using Service;
using Service.Contracts;
using Service.Contracts.Logger;

namespace JobBackground.DeviceAuto
{
    public class TimerJobDevice : BackgroundService
    {
        private readonly ILoggerManager logger;
        private readonly IDeviceControlService deviceControlService;
        private readonly IDataStatisticsService dataStatisticsService;
        private static List<LogDeviceStatusEntity> logDeviceStatusEntities = new List<LogDeviceStatusEntity>();

        public TimerJobDevice(ILoggerManager logger, IDeviceControlService deviceControlService, IDataStatisticsService dataStatisticsService)
        {
            this.logger = logger;
            this.deviceControlService = deviceControlService;
            this.dataStatisticsService = dataStatisticsService;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
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
            catch (Exception ex)
            {
                logger.LogError(ex.Message, new LogProcessModel()
                {
                    LoggerProcessType = LoggerProcessType.DeviceTimer,
                    LogMessageDetail = ex.ToString(),
                    ServiceName = $"{nameof(TimerJobDevice)} -> {nameof(ExecuteAsync)}",
                });
                throw;
            }
        }

        private async Task ToDoAsyncIsAuto()
        {
            var listTime = await deviceControlService.GetAllTimerAvailable();

            var listTimeCheckAuto = listTime.Where(t => t.IsAuto);

            var entitiesTurnOn = listTimeCheckAuto.Where(p => p!.OpenTimer!.Value.Minute <= DateTime.UtcNow.Minute && !p.IsSuccessON)!.ToList();
            var entitiesTurnOff = listTimeCheckAuto.Where(p => p!.ShutDownTimer!.Value.Minute <= DateTime.UtcNow.Minute && !p.IsSuccessOFF)!.ToList();

            if (entitiesTurnOn.Any())
            {
                foreach (var entity in entitiesTurnOn)
                {
                    logger.LogInformation($"On Device {entity.DeviceId}");
                    var model = new OnOffDeviceQueryModel()
                    {
                        ModuleId = entity.ModuleId,
                        DeviceId = entity.DeviceId,
                        DeviceType = entity.DeviceType,
                        DeviceNameNumber = entity.NameRef,
                        RequestOn = true,
                    };
                    var IsComplete = await deviceControlService.DeviceDriverOnOff(model);
                    logger.LogInformation($"On Device {IsComplete}");
                    if (IsComplete)
                    {
                        await deviceControlService.SuccessJobTurnOnDeviceTimer(entity.Id, entity.DeviceId);
                    }
                    logDeviceStatusEntities.Add(new LogDeviceStatusEntity()
                    {
                        DeviceName = entity.NameDeviceDriver,
                        RequestOn = true,
                        TypeOnOff = ((int)TypeOnOff.Timer),
                        ValueDate = DateTime.UtcNow,
                        TimerId = entity.Id,
                    });
                }
            }
            if (entitiesTurnOff.Any())
            {
                foreach (var entity in entitiesTurnOff)
                {
                    logger.LogInformation($"Off Device {entity.DeviceId}");
                    var model = new OnOffDeviceQueryModel()
                    {
                        ModuleId = entity.ModuleId,
                        DeviceId = entity.DeviceId,
                        DeviceType = entity.DeviceType,
                        DeviceNameNumber = entity.NameRef,
                        RequestOn = false,
                    };
                    var IsComplete = await deviceControlService.DeviceDriverOnOff(model);
                    logger.LogInformation($"On Device {IsComplete}");
                    if (IsComplete)
                    {
                        await deviceControlService.SuccessJobTurnOffDeviceTimer(entity.Id, entity.DeviceId);
                    }
                    logDeviceStatusEntities.Add(new LogDeviceStatusEntity()
                    {
                        DeviceName = entity.NameDeviceDriver,
                        RequestOn = false,
                        TypeOnOff = ((int)TypeOnOff.Timer),
                        ValueDate = DateTime.UtcNow,
                        TimerId = entity.Id,
                    });
                }
            }
            if (logDeviceStatusEntities.Any())
            {
                await dataStatisticsService.PushDataLogDeviceOnOff(logDeviceStatusEntities);
                logDeviceStatusEntities.Clear();
            }
        }
    }
}
