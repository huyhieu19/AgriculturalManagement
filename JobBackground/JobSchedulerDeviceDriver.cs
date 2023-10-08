using Common.Queries;
using Dapper;
using Database;
using Microsoft.Extensions.DependencyInjection;
using Models;
using Quartz;
using Service.Contracts;

namespace JobBackground
{
    public class JobSchedulerDeviceDriver
    {
        private readonly IScheduler _scheduler;
        private readonly ILoggerManager logger;
        private readonly IServiceProvider _serviceProvider;

        public JobSchedulerDeviceDriver(IScheduler scheduler, IServiceProvider serviceProvider, ILoggerManager logger)
        {
            _scheduler = scheduler;
            _serviceProvider = serviceProvider;
            this.logger = logger;
        }
        public async Task ScheduleJobs()
        {
            Console.WriteLine("Chay - lap lich quan trong");
            var dapperContext = _serviceProvider.GetRequiredService<DapperContext>();

            // Truy vấn danh sách công việc từ cơ sở dữ liệu
            var jobs = await GetDeviceDriverTurnOnTurnOffModels(dapperContext);

            // Đặt DapperContext vào JobDataMap
            var jobDataMap = new JobDataMap
            {
                { "DapperContext", dapperContext }
            };

            logger.LogInfomation("Start create job");
            foreach (var job in jobs)
            {
                // Lập lịch công việc mở may dựa trên OpenTimer
                var jobDetail1 = JobBuilder.Create<TurnOnDeviceDriver>()
                    .WithIdentity($"Job_TurnOn_{job.Id}")
                     .UsingJobData(jobDataMap)
                    .Build();
                var trigger1 = TriggerBuilder.Create()
                    .WithIdentity($"Trigger_TurnOn_{job.Id}")
                    .StartAt(job!.OpenTimer!.Value)
                    .Build();

                await _scheduler.ScheduleJob(jobDetail1, trigger1);

                // Lập lịch công việc đóng máy dựa trên ShutDownTimer
                var jobDetail2 = JobBuilder.Create<TurnOffDeviceDriver>()
                    .WithIdentity($"Job_TurnOff_{job.Id}")
                    .Build();

                var trigger2 = TriggerBuilder.Create()
                    .WithIdentity($"Trigger_TurnOff_{job.Id}")
                    .StartAt(job!.ShutDownTimer!.Value)
                    .Build();

                await _scheduler.ScheduleJob(jobDetail2, trigger2);
            }
            logger.LogInfomation("Finished create job");
        }


        public async Task RescheduleJobs(int jobId)
        {
            // Lấy DapperContext từ DI container
            var dapperContext = _serviceProvider.GetRequiredService<DapperContext>();
            // Truy vấn danh sách công việc từ cơ sở dữ liệu
            var jobs = await GetDeviceDriverTurnOnTurnOffModels(dapperContext);
            var job = jobs.FirstOrDefault(p => p.Id == jobId);
            // Đặt DapperContext vào JobDataMap
            var jobDataMap = new JobDataMap
            {
                { "DapperContext", dapperContext }
            };

            if (job != null)
            {
                var triggerKeyOn = new TriggerKey($"Trigger_TurnOn_{job.Id}");
                var existingTriggerOn = (ISimpleTrigger)_scheduler.GetTrigger(triggerKeyOn);

                logger.LogInfomation("Start update time create job");
                // Kiểm tra nếu thời gian khởi động đã thay đổi
                if (existingTriggerOn != null)
                {
                    // Lấy thời gian kế tiếp của công việc
                    var nextFireTime = existingTriggerOn.GetNextFireTimeUtc();
                    if (nextFireTime != null && nextFireTime.Value.Minute != job!.OpenTimer!.Value.Minute)
                    {
                        // Hủy bỏ công việc cũ
                        await _scheduler.UnscheduleJob(triggerKeyOn);

                        // Lên lịch công việc với thời gian mới
                        var jobDetail = JobBuilder.Create<TurnOnDeviceDriver>()
                            .WithIdentity($"Job_TurnOn_{job.Id}")
                            .UsingJobData(jobDataMap)
                            .Build();

                        var trigger = TriggerBuilder.Create()
                            .WithIdentity($"Trigger_TurnOn_{job.Id}")
                            .StartAt(job.OpenTimer.Value)
                            .Build();

                        await _scheduler.ScheduleJob(jobDetail, trigger);
                    }
                }

                var triggerKeyOff = new TriggerKey($"Trigger_TurnOff_{job.Id}");
                var existingTriggerOff = (ISimpleTrigger)_scheduler.GetTrigger(triggerKeyOff);

                // Kiểm tra nếu thời gian khởi động đã thay đổi
                if (existingTriggerOff != null)
                {
                    // Lấy thời gian kế tiếp của công việc
                    var nextFireTime = existingTriggerOff.GetNextFireTimeUtc();
                    if (nextFireTime != null && nextFireTime != job.OpenTimer)
                    {
                        // Hủy bỏ công việc cũ
                        await _scheduler.UnscheduleJob(triggerKeyOff);

                        // Lên lịch công việc với thời gian mới
                        var jobDetail = JobBuilder.Create<TurnOnDeviceDriver>()
                            .WithIdentity($"Job_TurnOff_{job.Id}")
                            .Build();

                        var trigger = TriggerBuilder.Create()
                            .WithIdentity($"Trigger_TurnOff_{job.Id}")
                            .StartAt(job!.ShutDownTimer!.Value)
                            .Build();

                        await _scheduler.ScheduleJob(jobDetail, trigger);
                    }
                }
                logger.LogInfomation("Finised update time create job");
            }
        }
        private async Task<IEnumerable<DeviceDriverTurnOnTurnOffModel>> GetDeviceDriverTurnOnTurnOffModels(DapperContext dapperContext)
        {
            var query = TimerDeviceDriverQuery.GetAllTimerSQL;
            IEnumerable<DeviceDriverTurnOnTurnOffModel> listTime;
            using (var connection = dapperContext.CreateConnection())
            {
                listTime = await connection.QueryAsync<DeviceDriverTurnOnTurnOffModel>(query);
            }
            return listTime;
        }
    }
}