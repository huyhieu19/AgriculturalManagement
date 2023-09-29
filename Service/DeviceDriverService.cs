using AutoMapper;
using Common.Queries;
using Dapper;
using Database;
using Entities;
using Models;
using Models.DeviceDriver.Timer;
using Repository.Contracts;
using Service.Contracts;

namespace Service
{
    public sealed class DeviceDriverService : IDeviceDriverService
    {
        private readonly IRepositoryManager repositoryManager;
        private readonly IMapper mapper;
        private readonly DapperContext dapperContext;


        public DeviceDriverService(IRepositoryManager repositoryManager, IMapper mapper, DapperContext dapperContext)
        {
            this.repositoryManager = repositoryManager;
            this.mapper = mapper;
            this.dapperContext = dapperContext;
        }

        public async Task CreateDeviceDriver(DeviceDriverCreateModel createModel)
        {
            DeviceDriverEntity entity = mapper.Map<DeviceDriverEntity>(createModel);
            repositoryManager.DeviceDriver.CreateDeviceDriver(entity);
            await repositoryManager.SaveAsync();
        }

        public async Task CreateTimer(TimerDeviceDriverCreateModel model)
        {
            var entity = mapper.Map<TimerDeviceDriverEntity>(model);
            repositoryManager.DeviceDriver.CreateTimer(entity);
            await repositoryManager.SaveAsync();
        }

        public async Task DeleteDeviceDriver(int Id)
        {

            repositoryManager.DeviceDriver.DeleteDeviceDriver(new DeviceDriverEntity() { Id = Id });
            await repositoryManager.SaveAsync();
        }

        public async Task DeleteTimer(int Id)
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

        public async Task<IEnumerable<TimerDeviceDriverDisplayModel>> GetAllTimer()
        {
            var query = TimerDeviceDriverQuery.GetAllTimerSQL;
            using (var connection = dapperContext.CreateConnection())
            {
                connection.Open();
                var result = await connection.QueryAsync<TimerDeviceDriverDisplayModel>(query);
                connection.Close();
                return result;
            }
        }

        public async Task<IEnumerable<TimerDeviceDriverDisplayModel>> GetAllTimerByDeviceId(int Id)
        {
            var query = TimerDeviceDriverQuery.GetAllTimerByDeviceDriverSQL;
            using (var connection = dapperContext.CreateConnection())
            {
                connection.Open();
                var result = await connection.QueryAsync<TimerDeviceDriverDisplayModel>(query, new { DeviceDriverId = Id });
                connection.Close();
                return result;
            }
        }

        public async Task<IEnumerable<TimerDeviceDriverDisplayModel>> GetAllTimerHistory()
        {
            var query = TimerDeviceDriverQuery.GetAllHistorySQL;
            using (var connection = dapperContext.CreateConnection())
            {
                connection.Open();
                var result = await connection.QueryAsync<TimerDeviceDriverDisplayModel>(query);
                connection.Close();
                return result;
            }
        }

        public async Task<IEnumerable<TimerDeviceDriverDisplayModel>> GetAllTimerHistoryByDeviceId(int Id)
        {
            var query = TimerDeviceDriverQuery.GetAllHistorySQL;
            using (var connection = dapperContext.CreateConnection())
            {
                connection.Open();
                var result = await connection.QueryAsync<TimerDeviceDriverDisplayModel>(query, new { Id = Id });
                connection.Close();
                return result;
            }
        }

        public async Task<IEnumerable<DeviceDriverDisplayModel>> GetDeviceDriverByZoneAsync(int Id)
        {
            return await repositoryManager.DeviceDriver.GetDeviceDriverByZoneAsync(Id);
        }

        public async Task<IEnumerable<DeviceDriverDisplayModel>> GetDeviceDriverNotInZoneAsync()
        {
            IEnumerable<DeviceDriverEntity> models = await repositoryManager.DeviceDriver.GetDeviceDriverNotInZoneAsync();
            return mapper.Map<IEnumerable<DeviceDriverDisplayModel>>(models);
        }

        public async Task RemoveDeviceDriver(int Id)
        {
            await repositoryManager.DeviceDriver.RemoveDeviceDriver(Id);
            await repositoryManager.SaveAsync();
        }

        public async Task UpdateInforDeviceDriver(DeviceDriverUpdateModel updateModel)
        {
            DeviceDriverEntity entity = mapper.Map<DeviceDriverEntity>(updateModel);
            repositoryManager.DeviceDriver.UpdateInforDeviceDriver(entity);
            await repositoryManager.SaveAsync();
        }

        public async Task UpdateTimer(TimerDeviceDriverDisplayModel model)
        {
            var entity = mapper.Map<TimerDeviceDriverEntity>(model);
            repositoryManager.DeviceDriver.UpdateTimer(entity);
            await repositoryManager.SaveAsync();
            //var query = TimerDeviceDriverQuery.UpdateTimerSQL;
            //var param = new DynamicParameters(model);
            //using (var connection = dapperContext.CreateConnection())
            //{
            //    connection.Open();
            //    using (var trans = connection.BeginTransaction())
            //    {
            //        await connection.ExecuteAsync(query, param, transaction: trans);
            //        trans.Commit();
            //    }
            //    connection.Close();
            //}
        }
    }
}
