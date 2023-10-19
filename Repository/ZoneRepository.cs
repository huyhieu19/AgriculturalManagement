using Common.Queries;
using Dapper;
using Database;
using Entities;
using Microsoft.EntityFrameworkCore;
using Models;
using Repository.Contracts;

namespace Repository
{
    public class ZoneRepository : RepositoryBase<ZoneEntity>, IZoneRepository
    {
        private readonly DapperContext dapperContext;

        public ZoneRepository(FactDbContext factDbContext, DapperContext dapperContext) : base(factDbContext)
        {
            this.dapperContext = dapperContext;
        }

        public void CreateZone(ZoneEntity entity) => Create(entity);

        public void DeleteZone(int id)
        {
            var entity = FindByCondition(p => p.Id == id, false).First();
            if (entity != null)
            {
                Delete(entity);
            }
        }

        public Task<IEnumerable<ImageEntity>> GetImagesByFarmId(int FarmId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ZoneEntity>> GetZones(int farmId, bool trackchanges) => await FindByCondition(p => p.FarmId == farmId, trackchanges).OrderBy(p => p.ZoneName).ToListAsync();


        public async void UpdateZone(ZoneUpdateModel model)
        {
            try
            {
                var param = new DynamicParameters(model);

                using (var connection = dapperContext.CreateConnection())
                {
                    connection.Open();
                    using (var trans = connection.BeginTransaction())
                    {
                        await connection.ExecuteAsync(FarmQuery.UpdateFarmSQL, param, transaction: trans);
                        trans.Commit();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
