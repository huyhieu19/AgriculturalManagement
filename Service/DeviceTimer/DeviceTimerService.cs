using AutoMapper;
using Common.Queries;
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
                DateCreated = DateTime.UtcNow,
                DateUpdated = null,
                DeviceId = model.DeviceDriverId,
                IsRemove = false,
                IsSuccessON = false,
                IsSuccessOFF = false,
                Note = model.Note,
                OpenTimer = model.OpenTimer,
                ShutDownTimer = model.ShutDownTimer,
            };

            await repositoryManager.DeviceDriver.CreateTimer(entity);
            return await repositoryManager.SaveAsync() > 0;
        }

        // Update information Timer
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


        public async Task<List<TimerDeviceDriverDisplayModel>> GetTimerAvailableOfUserForUI(string userId)
        {
            var entities = await repositoryManager.DeviceDriver.GetTimerAvailableOfUserForUI(userId);
            var models = mapper.Map<List<TimerDeviceDriverDisplayModel>>(entities);
            return models;
        }
        #endregion

        #region using Dapper
        public async Task<List<TimerDeviceDriverDisplayModel>> GetTimerAvailableOfUser(string userId)
        {
            var query = DeviceQuery.GetTimerAvailableOfUserSQL;
            IEnumerable<TimerDeviceDriverDisplayModel> result;
            using (var connection = dapperContext.CreateConnection())
            {
                connection.Open();
                result = await connection.QueryAsync<TimerDeviceDriverDisplayModel>(query, new { UserId = userId });
                connection.Close();
            }
            return result.Where(p => p.OpenTimer >= DateTime.UtcNow || p.ShutDownTimer >= DateTime.UtcNow).ToList();
        }

        // Hàm này dùng để set cho trạng thái của IsRemve = true
        public async Task<bool> RemoveTimer(int timerId, Guid deviceId)
        {
            logger.LogInformation($"DeviceDriver: Set status to complete --> DeviceDriverId: {deviceId}");
            var query = DeviceQuery.RemoveTimerSQL;
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

        public async Task<List<TimerDeviceDriverDisplayModel>> GetAllTimerHistoryByUser(string userId)
        {
            var query = DeviceQuery.GetTimerHistoryOfUserSQL;
            IEnumerable<TimerDeviceDriverDisplayModel> result;
            using (var connection = dapperContext.CreateConnection())
            {
                connection.Open();
                result = await connection.QueryAsync<TimerDeviceDriverDisplayModel>(query, new { UserId = userId });
                connection.Close();
            }
            return result.Where(p => (p.OpenTimer <= DateTime.UtcNow || p.ShutDownTimer <= DateTime.UtcNow) || (p.OpenTimer == null || p.ShutDownTimer == null)).OrderByDescending(p => p.OpenTimer).ThenByDescending(p => p.ShutDownTimer).ToList();
        }

        #endregion

    }
}