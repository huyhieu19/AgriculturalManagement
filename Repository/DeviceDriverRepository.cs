using AutoMapper;
using Common.Queries;
using Dapper;
using Database;
using Entities;
using Microsoft.EntityFrameworkCore;
using Models;
using Repository.Contracts;

namespace Repository
{
    public sealed class DeviceDriverRepository : RepositoryBase<DeviceDriverEntity>, IDeviceDriverRepository
    {
        private readonly DapperContext dapperContext;
        private readonly FactDbContext factDbContext;
        private readonly IMapper mapper;
        public DeviceDriverRepository(FactDbContext factDbContext, DapperContext dapperContext, IMapper mapper) : base(factDbContext)
        {
            this.mapper = mapper;
            this.dapperContext = dapperContext;
            this.factDbContext = factDbContext;
        }

        public void CreateDeviceDriver(DeviceDriverEntity createModel)
        {
            Create(createModel);
        }

        public void DeleteDeviceDriver(DeviceDriverEntity entity)
        {
            Delete(entity);
        }

        public async Task<IEnumerable<DeviceDriverDisplayModel>> GetDeviceDriverByZoneAsync(int Id)
        {

            var query = DeviceDriverQuery.GetDeviceDriverByZoneSQL;
            using (var connection = dapperContext.CreateConnection())
            {
                var result = await connection.QueryAsync<DeviceDriverDisplayModel>(query, new { ZoneId = Id });
                return result;
            }

        }

        public async Task<IEnumerable<DeviceDriverEntity>> GetDeviceDriverNotInZoneAsync()
        {
            var deviceDriverNotInZone = await FindByCondition(p => p.ZoneId == null, trackChanges: false).ToListAsync();
            return deviceDriverNotInZone;
        }

        public async Task RemoveDeviceDriver(int Id)
        {
            var entity = await FindByCondition(p => p.Id == Id, false).FirstOrDefaultAsync();
            entity!.ZoneId = null;
            Update(entity!);
        }

        public void UpdateInforDeviceDriver(DeviceDriverEntity updateModel)
        {
            Update(updateModel);
        }
        public async Task<IEnumerable<DeviceDriverEntity>> GetDeviceDriver()
        {
            return await FindAll(false).ToListAsync();
        }

        public async void CreateTimer(TimerDeviceDriverEntity entity)
        {
            await factDbContext.TimerDeviceDriverEntities.AddAsync(entity);
        }

        public void UpdateTimer(TimerDeviceDriverEntity entity)
        {
            factDbContext.TimerDeviceDriverEntities.Update(entity);
        }
    }
}