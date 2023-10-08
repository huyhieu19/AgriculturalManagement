using Common;
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
        private readonly DateTime timeZoneNow = SetTimeZone.GetTimeZone();

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
                await ToDoAsyncIsAuto();
                await Task.Delay(TimeSpan.FromSeconds(7), stoppingToken);
            }
        }
        /// <summary>
        /// Lấy giá trị list giá trị ngày giờ đóng mở và các giá trị ngưỡng
        /// </summary>
        private async Task<IEnumerable<DeviceDriverTurnOnTurnOffModel>> GetDeviceDriverTurnOnTurnOffModels()
        {
            logger.LogInfomation("Job background device driver --> start");
            var query = TimerDeviceDriverQuery.GetAllTimerSQL;
            IEnumerable<DeviceDriverTurnOnTurnOffModel> listTime;
            using (var connection = dapperContext.CreateConnection())
            {
                listTime = await connection.QueryAsync<DeviceDriverTurnOnTurnOffModel>(query);
            }
            logger.LogInfomation("Job background device driver --> end");
            return listTime;
        }
        // Hàm này dùng để tắt device driver ---> IsAcction = true
        private async Task DeviceDriverTurnOn(int DeviceDriverId)
        {
            logger.LogInfomation($"DeviceDriver: Turn on --> DeviceDriverId:  {DeviceDriverId}");
            var connection = dapperContext.CreateConnection();
            connection.Open();
            using (var trans = connection.BeginTransaction())
            {
                await connection.ExecuteAsync(TimerDeviceDriverQuery.UpdateTurnOnSQL, new { Id = DeviceDriverId }, transaction: trans);
                trans.Commit();
            }
            connection.Close();
        }
        // Hàm này dùng để tắt device driver ---> IsAcction = false
        private async Task DeviceDriverTurnOff(int DeviceDriverId)
        {
            logger.LogInfomation($"DeviceDriver: Turn off --> DeviceDriverId: {DeviceDriverId}");
            var connection = dapperContext.CreateConnection();
            connection.Open();
            using (var trans = connection.BeginTransaction())
            {
                await connection.ExecuteAsync(TimerDeviceDriverQuery.UpdateTurnOffSQL, new { Id = DeviceDriverId }, transaction: trans);
                trans.Commit();
            }
            connection.Close();
        }

        // Hàm này dùng để set cho trạng thái của IsRemve = true và IsSuccess = true
        private async Task DeleteTimer(int id)
        {
            logger.LogInfomation($"DeviceDriver: Set status to complete --> DeviceDriverId: {id}");
            var query = TimerDeviceDriverQuery.RemoveTimerSQL;
            var connection = dapperContext.CreateConnection();
            connection.Open();
            using (var trans = connection.BeginTransaction())
            {
                await connection.ExecuteAsync(query, new { Id = id }, transaction: trans);
                trans.Commit();
            }
            connection.Close();
        }

        private async Task ToDoAsyncIsAuto()
        {

            var listTime = await GetDeviceDriverTurnOnTurnOffModels();

            if (listTime != null && listTime.Any(p => p!.OpenTimer!.Value.Minute == timeZoneNow.Minute && p.IsAuto))
            {
                var entities = listTime.Where(p => p!.OpenTimer!.Value.Minute == timeZoneNow.Minute)!.ToList();

                foreach (var entity in entities)
                {
                    int? Id = entity!.DeviceDriverId;
                    await DeviceDriverTurnOn((int)Id);
                }
                await Task.Delay(TimeSpan.FromMinutes(30));
            }


            if (listTime != null && listTime.Any(p => p!.ShutDownTimer!.Value.Minute == timeZoneNow.Minute && p.IsAuto))
            {
                var entities = listTime.Where(p => p!.ShutDownTimer!.Value.Minute == timeZoneNow.Minute)!.ToList();

                foreach (var entity in entities)
                {
                    int? Id = entity!.DeviceDriverId;
                    await DeviceDriverTurnOff((int)Id);
                    await DeleteTimer((int)Id);
                }
                await Task.Delay(TimeSpan.FromMinutes(30));
            }
        }
    }
}
