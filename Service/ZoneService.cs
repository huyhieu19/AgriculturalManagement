using AutoMapper;
using Common.Queries;
using Dapper;
using Database;
using Entities;
using Models;
using Repository.Contracts;
using Service.Contracts;

namespace Service
{
    public sealed class ZoneService : IZoneService
    {
        private readonly IRepositoryManager repositoryManager;
        private readonly IMapper mapper;
        private readonly DapperContext dapperContext;

        public ZoneService(IRepositoryManager repositoryManager, IMapper mapper, DapperContext dapperContext)
        {
            this.repositoryManager = repositoryManager;
            this.mapper = mapper;
            this.dapperContext = dapperContext;
        }

        public async Task<bool> AddZone(ZoneCreateModel createModel)
        {
            var entity = mapper.Map<ZoneEntity>(createModel);
            repositoryManager.Zone.CreateZone(entity);
            await repositoryManager.SaveAsync();
            return true;
        }

        public async Task<IEnumerable<ZoneDisplayModel>> GetZones(int farmId, bool trackChanges)
        {
            string query = ZoneQuery.GetZoneSQL;
            IEnumerable<ZoneDisplayModel> result;
            using (var connection = dapperContext.CreateConnection())
            {
                result = await connection.QueryAsync<ZoneDisplayModel>(query, new { FarmId = farmId });
            }
            return result;
        }

        public async Task<bool> RemoveZone(int id)
        {
            repositoryManager.Zone.DeleteZone(id);
            var isChange = await repositoryManager.SaveAsync();
            return isChange > 0;
        }

        public async Task<bool> UpdateZone(ZoneUpdateModel updateModel)
        {
            try
            {
                var entity = mapper.Map<ZoneEntity>(updateModel);
                repositoryManager.Zone.UpdateZone(entity);
                int isChange = await repositoryManager.SaveAsync();
                return isChange > 0;
            }
            catch (Exception ex)
            {
                throw new AggregateException(ex.Message);
            }
        }
    }
}
