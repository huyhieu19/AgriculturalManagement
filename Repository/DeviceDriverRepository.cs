using Database;
using Entities;
using Entities.ESP;
using Repository.Contracts;

namespace Repository
{
    public sealed class DeviceDriverRepository : RepositoryBase<DeviceEntity>, IDeviceDriverRepository
    {
        private readonly DapperContext dapperContext;
        private readonly FactDbContext factDbContext;
        public DeviceDriverRepository(FactDbContext factDbContext, DapperContext dapperContext) : base(factDbContext)
        {
            this.dapperContext = dapperContext;
            this.factDbContext = factDbContext;
        }

        //public void CreateDeviceDriver(DeviceDriverEntity createModel)
        //{
        //    Create(createModel);
        //}

        //public void DeleteDeviceDriver(DeviceDriverEntity entity)
        //{
        //    Delete(entity);
        //}

        //public async Task<IEnumerable<DeviceDriverEntity>> GetDeviceDriverByZoneAsync(int Id)
        //{

        //    //var query = DeviceDriverQuery.GetDeviceDriverByZoneSQL;
        //    //using (var connection = dapperContext.CreateConnection())
        //    //{
        //    //    var result = await connection.QueryAsync<DeviceDriverDisplayModel>(query, new { ZoneId = Id });
        //    //    return result;
        //    //}

        //    var entity = await FindByCondition(src => src.ZoneId == Id, false).ToListAsync();
        //    return entity;
        //}

        //public async Task<IEnumerable<DeviceDriverEntity>> GetDeviceDriverNotInZoneAsync()
        //{
        //    var deviceDriverNotInZone = await FindByCondition(p => p.ZoneId == null, trackChanges: false).ToListAsync();
        //    return deviceDriverNotInZone;
        //}

        //public async Task RemoveDeviceDriver(int Id)
        //{
        //    var entity = await FindByCondition(p => p.Id == Id, false).FirstOrDefaultAsync();
        //    entity!.ZoneId = null;
        //    Update(entity!);
        //}

        //public void UpdateInforDeviceDriver(DeviceDriverEntity updateModel)
        //{
        //    Update(updateModel);
        //}
        //public async Task<IEnumerable<DeviceDriverEntity>> GetDeviceDriver()
        //{
        //    return await FindAll(false).ToListAsync();
        //}

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