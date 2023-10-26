using AutoMapper;
using Entities;
using Microsoft.Extensions.Hosting;
using Models;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using Service.Contracts.ESP;
using Service.ESP;
using Unity;
using Unity.Injection;
using Unity.Lifetime;

namespace JobBackground
{

    public class JobSchedulerDeviceDriver : BackgroundService
    {
        private readonly IScheduler _scheduler;
        //private readonly ILoggerManager logger;
        //private readonly IServiceProvider _serviceProvider;

        public JobSchedulerDeviceDriver()
        {
            ISchedulerFactory schedulerFactory = new StdSchedulerFactory();
            _scheduler = schedulerFactory.GetScheduler().Result;
        }


        public async Task ScheduleJobs1()
        {

            // Set up Unity and register dependencies
            var container = new UnityContainer();
            //container.RegisterType<IDataStatisticsService, DataStatisticsService>();

            //container.RegisterType<IEspBackgroundProcessService, EspBackgroundProcessService>();

            //// Register AutoMapper with a Singleton lifetime
            //container.RegisterType<IMapper>(new ContainerControlledLifetimeManager(), new InjectionFactory(c => new MapperConfiguration(cfg =>
            //{
            //    // Define your mapping profiles here
            //    cfg.AddProfile<MappingProfile>();
            //}).CreateMapper()));

            //var config = ConfigurationBuilder.AddJsonFile("appsettings.json").Build();

            //// Register IConfiguration
            //container.RegisterInstance<IConfiguration>(config);

            // Configure Quartz.NET to use the custom job factory
            var schedulerFactory = new StdSchedulerFactory();
            var scheduler = schedulerFactory.GetScheduler().Result;
            scheduler.JobFactory = new IntegrationJobFactory(container);


            var job = JobBuilder.Create<UPload>()
                    .WithIdentity($"Job_TurnOn")
                    .Build();
            var trigger = TriggerBuilder.Create()
            .WithIdentity($"Trigger_TurnOn")
            .StartNow()
            .WithSimpleSchedule(x => x
                .WithIntervalInSeconds(5) // Chạy mỗi 5 giây
                .RepeatForever())          // Lặp vô hạn
            .Build();

            // Lên lịch công việc với trigger
            await _scheduler.ScheduleJob(job, trigger);
            await _scheduler.Start();
            // Don't forget to shutdown the scheduler when the application is stopping
            AppDomain.CurrentDomain.ProcessExit += (sender, e) => _scheduler.Shutdown().Wait();
        }

        public async Task DeleteScheduleJobs1()
        {
            // Tìm trigger bằng ID (nếu bạn cần hủy bất kỳ công việc cụ thể nào)

            var triggerKey = new TriggerKey("Trigger_TurnOn");
            var trigger = await _scheduler.GetTrigger(triggerKey);

            if (trigger != null)
            {
                // Xóa trigger
                await _scheduler.UnscheduleJob(triggerKey);
            }
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Task.Run(() => ScheduleJobs1());
            return Task.CompletedTask;
        }
        internal sealed class IntegrationJobFactory : IJobFactory
        {
            private readonly IUnityContainer _container;

            public IntegrationJobFactory(IUnityContainer container)
            {
                _container = container;
            }

            public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
            {
                var jobDetail = bundle.JobDetail;
                var job = (IJob)_container.Resolve(jobDetail.JobType);
                return job;
            }

            public void ReturnJob(IJob job)
            {
            }
        }



        //public async Task ScheduleJobs()
        //{
        //    Console.WriteLine("Chay - lap lich quan trong");
        //    var dapperContext = _serviceProvider.GetRequiredService<DapperContext>();

        //    // Truy vấn danh sách công việc từ cơ sở dữ liệu
        //    var jobs = await GetDeviceDriverTurnOnTurnOffModels(dapperContext);

        //    // Đặt DapperContext vào JobDataMap
        //    var jobDataMap = new JobDataMap
        //    {
        //        { "DapperContext", dapperContext }
        //    };

        //    logger.LogInfomation("Start create job");
        //    foreach (var job in jobs)
        //    {
        //        // Lập lịch công việc mở may dựa trên OpenTimer
        //        var jobDetail1 = JobBuilder.Create<TurnOnDeviceDriver>()
        //            .WithIdentity($"Job_TurnOn_{job.Id}")
        //             .UsingJobData(jobDataMap)
        //            .Build();
        //        var trigger1 = TriggerBuilder.Create()
        //            .WithIdentity($"Trigger_TurnOn_{job.Id}")
        //            .StartAt(job!.OpenTimer!.Value)
        //            .Build();

        //        await _scheduler.ScheduleJob(jobDetail1, trigger1);

        //        // Lập lịch công việc đóng máy dựa trên ShutDownTimer
        //        var jobDetail2 = JobBuilder.Create<TurnOffDeviceDriver>()
        //            .WithIdentity($"Job_TurnOff_{job.Id}")
        //            .Build();

        //        var trigger2 = TriggerBuilder.Create()
        //            .WithIdentity($"Trigger_TurnOff_{job.Id}")
        //            .StartAt(job!.ShutDownTimer!.Value)
        //            .Build();

        //        await _scheduler.ScheduleJob(jobDetail2, trigger2);
        //    }
        //    logger.LogInfomation("Finished create job");
        //}


        //public async Task RescheduleJobs(int jobId)
        //{
        //    // Lấy DapperContext từ DI container
        //    var dapperContext = _serviceProvider.GetRequiredService<DapperContext>();
        //    // Truy vấn danh sách công việc từ cơ sở dữ liệu
        //    var jobs = await GetDeviceDriverTurnOnTurnOffModels(dapperContext);
        //    var job = jobs.FirstOrDefault(p => p.Id == jobId);
        //    // Đặt DapperContext vào JobDataMap
        //    var jobDataMap = new JobDataMap
        //    {
        //        { "DapperContext", dapperContext }
        //    };

        //    if (job != null)
        //    {
        //        var triggerKeyOn = new TriggerKey($"Trigger_TurnOn_{job.Id}");
        //        var existingTriggerOn = (ISimpleTrigger)_scheduler.GetTrigger(triggerKeyOn);

        //        logger.LogInfomation("Start update time create job");
        //        // Kiểm tra nếu thời gian khởi động đã thay đổi
        //        if (existingTriggerOn != null)
        //        {
        //            // Lấy thời gian kế tiếp của công việc
        //            var nextFireTime = existingTriggerOn.GetNextFireTimeUtc();
        //            if (nextFireTime != null && nextFireTime.Value.Minute != job!.OpenTimer!.Value.Minute)
        //            {
        //                // Hủy bỏ công việc cũ
        //                await _scheduler.UnscheduleJob(triggerKeyOn);

        //                // Lên lịch công việc với thời gian mới
        //                var jobDetail = JobBuilder.Create<TurnOnDeviceDriver>()
        //                    .WithIdentity($"Job_TurnOn_{job.Id}")
        //                    .UsingJobData(jobDataMap)
        //                    .Build();

        //                var trigger = TriggerBuilder.Create()
        //                    .WithIdentity($"Trigger_TurnOn_{job.Id}")
        //                    .StartAt(job.OpenTimer.Value)
        //                    .Build();

        //                await _scheduler.ScheduleJob(jobDetail, trigger);
        //            }
        //        }

        //        var triggerKeyOff = new TriggerKey($"Trigger_TurnOff_{job.Id}");
        //        var existingTriggerOff = (ISimpleTrigger)_scheduler.GetTrigger(triggerKeyOff);

        //        // Kiểm tra nếu thời gian khởi động đã thay đổi
        //        if (existingTriggerOff != null)
        //        {
        //            // Lấy thời gian kế tiếp của công việc
        //            var nextFireTime = existingTriggerOff.GetNextFireTimeUtc();
        //            if (nextFireTime != null && nextFireTime != job.OpenTimer)
        //            {
        //                // Hủy bỏ công việc cũ
        //                await _scheduler.UnscheduleJob(triggerKeyOff);

        //                // Lên lịch công việc với thời gian mới
        //                var jobDetail = JobBuilder.Create<TurnOnDeviceDriver>()
        //                    .WithIdentity($"Job_TurnOff_{job.Id}")
        //                    .Build();

        //                var trigger = TriggerBuilder.Create()
        //                    .WithIdentity($"Trigger_TurnOff_{job.Id}")
        //                    .StartAt(job!.ShutDownTimer!.Value)
        //                    .Build();

        //                await _scheduler.ScheduleJob(jobDetail, trigger);
        //            }
        //        }
        //        logger.LogInfomation("Finised update time create job");
        //    }
        //}
        //private async Task<IEnumerable<DeviceDriverTurnOnTurnOffModel>> GetDeviceDriverTurnOnTurnOffModels(DapperContext dapperContext)
        //{
        //    var query = TimerDeviceDriverQuery.GetAllTimerSQL;
        //    IEnumerable<DeviceDriverTurnOnTurnOffModel> listTime;
        //    using (var connection = dapperContext.CreateConnection())
        //    {
        //        listTime = await connection.QueryAsync<DeviceDriverTurnOnTurnOffModel>(query);
        //    }
        //    return listTime;
        //}
    }
}