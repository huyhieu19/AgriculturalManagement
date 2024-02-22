using Common.Enum;
using Entities.LogProcess;
using EnumsNET;
using Microsoft.Extensions.Hosting;
using Models;
using Models.DeviceControl;
using Service.Contracts;
using Service.Contracts.Logger;

namespace JobBackground.DeviceAuto
{
    public class TimerJobDevice : BackgroundService
    {
        private readonly ILoggerManager logger;
        private readonly IDeviceControlService deviceControlService;
        private static List<LogDeviceStatusEntity> logDeviceStatusEntities = new List<LogDeviceStatusEntity>();

        public TimerJobDevice(ILoggerManager logger, IDeviceControlService deviceControlService)
        {
            this.logger = logger;
            this.deviceControlService = deviceControlService;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // Run every 10 seconds
            while (true)
            {
                try
                {
                    logger.LogInformation("3. Start Timer Job service");
                    await ToDoAsyncIsAutoTimer(); // Simulate work.
                    logger.LogInformation("3. End Timer Job service");
                    await Task.Delay(TimeSpan.FromSeconds(5));
                }
                catch (Exception ex)
                {
                    // ghi lại log lỗi của job
                    logger.LogError(ex.Message, new LogProcessModel()
                    {
                        LoggerProcessType = LoggerProcessType.DeviceTimer,
                        LogMessageDetail = ex.ToString(),
                        ServiceName = $"{nameof(TimerJobDevice)} -> {nameof(ExecuteAsync)}",
                    });
                    await Task.Delay(TimeSpan.FromSeconds(30));
                    throw;
                }
            }

        }

        private async Task ToDoAsyncIsAutoTimer()
        {
            try
            {
                var listTime = await deviceControlService.GetAllTimerAvailable();

                var listTimeCheckAuto = listTime.Where(t => t.IsAuto);

                //var entitiesTurnOn = listTimeCheckAuto.Where(p => p.OpenTimer != null && p!.OpenTimer!.Value.Ceiling(TimeSpan.FromMinutes(1)) == DateTime.UtcNow.Ceiling(TimeSpan.FromMinutes(1)) && !p.IsSuccessON)!.ToList();
                //var entitiesTurnOff = listTimeCheckAuto.Where(p => p.ShutDownTimer != null && p!.ShutDownTimer!.Value.Ceiling(TimeSpan.FromMinutes(1)) == DateTime.UtcNow.Ceiling(TimeSpan.FromMinutes(1)) && !p.IsSuccessOFF)!.ToList();
                var entitiesTurnOn = listTimeCheckAuto.Where(p => p.OpenTimer != null && p!.OpenTimer!.Value.Minute == DateTime.UtcNow.Minute && !p.IsSuccessON)!.ToList();
                var entitiesTurnOff = listTimeCheckAuto.Where(p => p.ShutDownTimer != null && p!.ShutDownTimer!.Value.Minute == DateTime.UtcNow.Minute && !p.IsSuccessOFF)!.ToList();

                if (entitiesTurnOn.Any())
                {
                    foreach (var entity in entitiesTurnOn)
                    {
                        logger.LogInformation($"On Device {entity.DeviceId}");
                        var model = new OnOffDeviceQueryModel()
                        {
                            ModuleId = entity.ModuleId,
                            DeviceId = entity.DeviceId,
                            DeviceName = entity.DeviceName,
                            DeviceType = entity.DeviceType,
                            DeviceNameNumber = ((FunctionDeviceType)entity.NameRef).AsString(EnumFormat.Description)!,
                            RequestOn = true,
                        };
                        // thực hiện hành động đóng/mở
                        var IsComplete = await deviceControlService.DeviceDriverOnOff(model);
                        logger.LogInformation($"On Device {IsComplete}");
                        if (IsComplete)
                        {
                            IsComplete = IsComplete && await deviceControlService.SuccessJobTurnOnOffDeviceTimer(entity.Id, entity.DeviceId, model.RequestOn);
                        }
                        // ghi log đã hoàn thành/chưa hoàn thành thời gian hẹn hiện tại
                        logDeviceStatusEntities.Add(new LogDeviceStatusEntity()
                        {
                            DeviceName = entity.DeviceName,
                            RequestOn = model.RequestOn,
                            TypeOnOff = ((int)TypeOnOff.Timer),
                            ValueDate = DateTime.UtcNow,
                            TimerId = entity.Id,
                            IsSuccess = IsComplete
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
                            DeviceName = entity.DeviceName,
                            DeviceId = entity.DeviceId,
                            DeviceType = entity.DeviceType,
                            DeviceNameNumber = entity.NameRef.ToString(),
                            RequestOn = false,
                        };
                        // thực hiện hành động đóng/mở
                        var IsComplete = await deviceControlService.DeviceDriverOnOff(model);
                        logger.LogInformation($"On Device {IsComplete}");

                        if (IsComplete)
                        {
                            IsComplete = IsComplete && await deviceControlService.SuccessJobTurnOnOffDeviceTimer(entity.Id, entity.DeviceId, false);
                        }
                        // ghi log đã hoàn thành/ chưa hoàn thành thời gian hẹn hiện tại
                        logDeviceStatusEntities.Add(new LogDeviceStatusEntity()
                        {
                            DeviceName = entity.DeviceName,
                            RequestOn = false,
                            TypeOnOff = ((int)TypeOnOff.Timer),
                            ValueDate = DateTime.UtcNow,
                            TimerId = entity.Id,
                            IsSuccess = IsComplete
                        });
                    }
                }
                // ghi log đã đóng mở thiết bị nào!
                if (logDeviceStatusEntities.Any())
                {
                    await logger.LogMultipleOnOffDevice(logDeviceStatusEntities);
                    logDeviceStatusEntities.Clear();
                }
            }
            catch
            {
                logger.LogInformation("Error async timer");
                throw;
            }
        }
    }
}
