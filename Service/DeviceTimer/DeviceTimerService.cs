using AutoMapper;
using Common.Queries;
using Common.TimeHelper;
using Dapper;
using Database;
using Entities;
using Models.DeviceTimer;
using Repository.Contracts;
using Service.Contracts.DeviceTimer;
using Service.Contracts.Logger;

namespace Service.DeviceTimer
{
    public sealed class DeviceTimerService : IDeviceTimerService
    {
        private readonly IRepositoryManager repositoryManager;
        private readonly IMapper mapper;
        private readonly DapperContext dapperContext;
        private readonly ILoggerManager logger;

        public DeviceTimerService(IRepositoryManager repositoryManager,
            IMapper mapper,
            DapperContext dapperContext,
            ILoggerManager logger)
        {
            this.repositoryManager = repositoryManager;
            this.mapper = mapper;
            this.dapperContext = dapperContext;
            this.logger = logger;
        }

        #region using EF Core
        public async Task<bool> CreateTimer(TimerDeviceDriverCreateModel model)
        {
            var entity = new TimerDeviceEntity()
            {
                DateCreated = SetTimeZone.GetDateTimeVN(),
                DateUpdated = null,
                DeviceId = model.DeviceDriverId,
                IsRemove = false,
                IsSuccess = false,
                Note = model.Note,
                OpenTimer = model.OpenTimer,
                ShutDownTimer = model.ShutDownTimer
            };

            repositoryManager.DeviceDriver.CreateTimer(entity);
            return await repositoryManager.SaveAsync() > 0;
        }

        public async Task<bool> UpdateTimer(TimerDeviceDriverUpdateModel model)
        {
            return await repositoryManager.DeviceDriver.UpdateTimer(model);
        }

        public async Task<List<TimerDeviceDriverDisplayModel>> GetAllTimerHistoryByDeviceId(Guid deviceId)
        {
            var entities = await repositoryManager.DeviceDriver.GetAllTimerHistoryByDeviceId(deviceId);
            var models = mapper.Map<List<TimerDeviceDriverDisplayModel>>(entities);
            return models;
        }

        public async Task<List<TimerDeviceDriverDisplayModel>> GetAllTimerHistory()
        {
            var entities = await repositoryManager.DeviceDriver.GetAllTimerHistory();
            var models = mapper.Map<List<TimerDeviceDriverDisplayModel>>(entities);
            return models;
        }
        #endregion

        #region using Dapper
        public async Task<IEnumerable<TimerDeviceDriverDisplayModel>> GetTimerAvailableOfUser(string userId)
        {
            var query = TimerDeviceDriverQuery.GetTimerAvailableOfUserSQL;
            using (var connection = dapperContext.CreateConnection())
            {
                connection.Open();
                var result = await connection.QueryAsync<TimerDeviceDriverDisplayModel>(query, new { UserId = userId });
                connection.Close();
                return result;
            }
        }

        // Hàm này dùng để set cho trạng thái của IsRemve = true
        public async Task<bool> RemoveTimer(int timerId, Guid deviceId)
        {
            logger.LogInformation($"DeviceDriver: Set status to complete --> DeviceDriverId: {timerId}");
            var query = TimerDeviceDriverQuery.RemoveTimerSQL;
            var connection = dapperContext.CreateConnection();
            connection.Open();
            int execute;
            using (var trans = connection.BeginTransaction())
            {
                execute = await connection.ExecuteAsync(query, new { Id = timerId, DeviceId = deviceId }, transaction: trans);
                trans.Commit();
            }
            connection.Close();
            return execute > 0;
        }
        // Hàm này dùng để set cho trạng thái của IsRemve = true và IsSuccess = true
        public async Task<bool> SuccessJobTimer(int timerId, Guid deviceId)
        {
            logger.LogInformation($"DeviceDriver: Set status to complete --> DeviceDriverId: {timerId}");
            var query = TimerDeviceDriverQuery.SuccessTimerSQL;
            var connection = dapperContext.CreateConnection();
            connection.Open();
            int execute;
            using (var trans = connection.BeginTransaction())
            {
                execute = await connection.ExecuteAsync(query, new { Id = timerId, DeviceId = deviceId }, transaction: trans);
                trans.Commit();
            }
            connection.Close();
            return execute > 0;
        }

        public async Task<IEnumerable<TimerDeviceDriverDisplayModel>> GetAllTimerAvailable()
        {
            var query = TimerDeviceDriverQuery.GetAllTimerAvailable;
            using (var connection = dapperContext.CreateConnection())
            {
                connection.Open();
                var result = await connection.QueryAsync<TimerDeviceDriverDisplayModel>(query);
                connection.Close();
                return result;
            }
        }
        #endregion
    }
}