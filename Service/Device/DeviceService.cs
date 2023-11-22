using AutoMapper;
using Common.Enum;
using Common.Queries;
using Dapper;
using Database;
using Models.Device;
using Repository.Contracts;
using Service.Contracts.Device;

namespace Service.Device
{
    public sealed class DeviceService : IDeviceService
    {
        private readonly IRepositoryManager repository;
        private readonly IMapper mapper;
        private readonly DapperContext dapperContext;
        public DeviceService(IRepositoryManager repository, IMapper mapper, DapperContext dapperContext)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.dapperContext = dapperContext;
        }

        public async Task<bool> AddDeviceToZone(Guid deviceId, int zoneId)
        {
            await repository.Device.AddDeviceToZone(deviceId, zoneId);
            return await repository.SaveAsync() > 0;
        }

        public async Task<bool> RemoveDeviceFromZone(Guid deviceId, int zoneId)
        {
            await repository.Device.RemoveDeviceFromZone(deviceId, zoneId);
            return await repository.SaveAsync() > 0;
        }

        private async Task<List<DeviceDisplayModel>> GetDevicesOnZone(int zoneId)
        {
            var deviceEntities = await repository.Device.GetDevicesOnZone(zoneId);
            var deviceModels = mapper.Map<List<DeviceDisplayModel>>(deviceEntities);
            return deviceModels;
        }

        public async Task<List<DeviceDisplayModel>> GetDevicesControlOnZone(int zoneId)
        {
            var devices = await GetDevicesOnZone(zoneId);
            var result = devices.Where(prop => prop.DeviceType == DeviceType.W.ToString()).ToList();
            return result;
        }

        public async Task<List<DeviceDisplayModel>> GetDevicesInstrumentationOnZone(int zoneId)
        {
            var devices = await GetDevicesOnZone(zoneId);
            var result = devices.Where(prop => prop.DeviceType == DeviceType.R.ToString()).ToList();
            return result;
        }
        public async Task<bool> SetAutoDevice(Guid deviceId, bool IsAuto)
        {
            var queryAutoOn = DeviceQuery.UpdateIsAutoSQL;
            var connection = dapperContext.CreateConnection();
            connection.Open();
            int execute;
            using (var trans = connection.BeginTransaction())
            {
                execute = await connection.ExecuteAsync(queryAutoOn, new { Id = deviceId, IsAuto = (IsAuto == true) ? 1 : 0 }, transaction: trans);
                trans.Commit();
            }
            connection.Close();
            return execute > 0;
        }
    }
}
