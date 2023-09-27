using AutoMapper;
using Common.Queries;
using Dapper;
using Database;
using Entities;
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

        public Task CreateDeviceDriver(DeviceDriverCreateModel createModel)
        {
            throw new NotImplementedException();
        }

        public Task DeleteDeviceDriver(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<DeviceDriverDisplayModel>> GetDeviceDriverByZoneAsync(int Id)
        {
            /**
             * Using --- EF CORE
                var deviceDriverByZone = await FindByCondition(p => p.ZoneId == Id, trackChanges: false).ToListAsync();
                ZoneEntity? zone = await factDbContext.ZoneEntityEntities.Where(p => p.Id == Id).FirstOrDefaultAsync();
                List<DeviceDriverDisplayModel> result = new List<DeviceDriverDisplayModel>();
                for (int i = 0; i < deviceDriverByZone.Count; i++)
                {
                    DeviceDriverTypeEntity? deviceDriverType = await factDbContext.DeviceDriversTypeEntities.Where(p => p.Id == deviceDriverByZone[i].DeviceDriverTypeId).FirstOrDefaultAsync();

                    var deviceDisplayModel = mapper.Map<DeviceDriverDisplayModel>(deviceDriverByZone[i]);

                    deviceDisplayModel.ZoneId = deviceDriverByZone[i].ZoneId;
                    deviceDisplayModel.ZoneName = zone!.ZoneName ?? null;
                    deviceDisplayModel.DescriptionZone = zone!.Description ?? null;

                    deviceDisplayModel.DeviceDriverTypeId = deviceDriverByZone[i].DeviceDriverTypeId ?? null;
                    deviceDisplayModel.DeviceDriverTypeName = deviceDriverType!.Name ?? null;
                    deviceDisplayModel.DeviceDriverTypeDescription = deviceDriverType!.Description ?? null;
                    deviceDisplayModel.DeviceDriverTypeManufacturer = deviceDriverType!.Manufacturer ?? null;
                    deviceDisplayModel.DeviceDriverTypeImageUrl = deviceDriverType!.ImageUrl ?? null;

                    result.Add(deviceDisplayModel);
                }
                return result;
            */
            /// -- Using Dapper
            var query = DeviceDriverQuery.GetDeviceDriverByZone;
            using (var connection = dapperContext.CreateConnection())
            {
                var result = await connection.QueryAsync<DeviceDriverDisplayModel>(query, new { ZoneId = Id });
                return result;
            }

        }

        public Task<IEnumerable<DeviceDriverDisplayModel>> GetDeviceDriverNotInZoneAsync()
        {
            throw new NotImplementedException();
        }

        public Task RemoveDeviceDriver(int Id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateInforDeviceDriver(DeviceDriverUpdateModel updateModel)
        {
            throw new NotImplementedException();
        }
    }
}
