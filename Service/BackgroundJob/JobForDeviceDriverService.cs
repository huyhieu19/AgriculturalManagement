using Common.Queries;
using Dapper;
using Database;
using Microsoft.Extensions.Hosting;
using Models;
using Service.Contracts;

namespace Service.BackgroundJob
{
    public class JobForDeviceDriverService : BackgroundService
    {
        private readonly ILoggerManager logger;
        private readonly DapperContext dapperContext;


        public JobForDeviceDriverService(ILoggerManager logger, DapperContext dapperContext)
        {
            this.logger = logger;
            this.dapperContext = dapperContext;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // Run every 5 seconds
            while (!stoppingToken.IsCancellationRequested)
            {
                logger.LogInfomation("1 Background job is running...");
                await ToDoAsyncIsAuto();
                logger.LogInfomation("2 Background job is running...");
                await Task.Delay(TimeSpan.FromSeconds(7), stoppingToken);
            }
        }

        private async Task<IEnumerable<DeviceDriverTurnOnTurnOffModel>> GetDeviceDriverTurnOnTurnOffModels()
        {
            logger.LogInfomation("GetDeviceDriverTurnOnTurnOffModels - start");
            var query = DeviceDriverQuery.GetTurnOnAndTurnOffSQL;
            IEnumerable<DeviceDriverTurnOnTurnOffModel> listTime;
            using (var connection = dapperContext.CreateConnection())
            {
                listTime = await connection.QueryAsync<DeviceDriverTurnOnTurnOffModel>(query);
            }
            logger.LogInfomation("GetDeviceDriverTurnOnTurnOffModels - end");
            return listTime;
        }

        private async Task DeviceDriverTurnOn(int DeviceDriverId)
        {
            logger.LogInfomation($"Turn on --> DeviceDriverId:  {DeviceDriverId}");
            using (var connection = dapperContext.CreateConnection())
            {
                connection.Open();
                using (var trans = connection.BeginTransaction())
                {
                    await connection.ExecuteAsync(TimerDeviceDriverQuery.UpdateTurnOnSQL, new { Id = DeviceDriverId }, transaction: trans);
                    trans.Commit();
                }
                connection.Close();
            }
        }
        private async Task DeviceDriverTurnOff(int DeviceDriverId)
        {
            logger.LogInfomation($"Turn off --> DeviceDriverId: {DeviceDriverId}");
            using (var connection = dapperContext.CreateConnection())
            {
                connection.Open();
                using (var trans = connection.BeginTransaction())
                {
                    await connection.ExecuteAsync(TimerDeviceDriverQuery.UpdateTurnOffSQL, new { Id = DeviceDriverId }, transaction: trans);
                    trans.Commit();
                }
                connection.Close();
            }
        }

        private async Task DeleteTimer(int Id)
        {
            var query = TimerDeviceDriverQuery.RemoveTimerSQL;
            using (var connection = dapperContext.CreateConnection())
            {
                connection.Open();
                using (var trans = connection.BeginTransaction())
                {
                    await connection.ExecuteAsync(query, new { Id = Id }, transaction: trans);
                    trans.Commit();
                }
                connection.Close();
            }
        }

        private async Task ToDoAsyncIsAuto()
        {

            var listTime = await GetDeviceDriverTurnOnTurnOffModels();

            if (listTime != null && listTime.Any(p => p.OpenTimer.Minute == DateTime.Now.Minute && p.IsAuto))
            {
                var entities = listTime.Where(p => p.OpenTimer.Minute == DateTime.Now.Minute)!.ToList();

                foreach (var entity in entities)
                {
                    int? Id = entity!.DeviceDriverId;
                    if (Id is not null)
                    {
                        await DeviceDriverTurnOn((int)Id);
                    }
                }
                await Task.Delay(TimeSpan.FromMinutes(1));
            }


            if (listTime != null && listTime.Any(p => p.ShutDownTimer.Minute == DateTime.Now.Minute && p.IsAuto))
            {
                var entities = listTime.Where(p => p.ShutDownTimer.Minute == DateTime.Now.Minute)!.ToList();

                foreach (var entity in entities)
                {
                    int? Id = entity!.DeviceDriverId;
                    if (Id is not null)
                    {
                        await DeviceDriverTurnOff((int)Id);
                        await DeleteTimer((int)Id);
                    }

                }
                await Task.Delay(TimeSpan.FromMinutes(1));
            }
        }
    }
}
