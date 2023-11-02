using Common.Queries;
using Dapper;
using Database;
using Entities;
using Microsoft.EntityFrameworkCore;
using Models;
using Repository.Contracts;

namespace Repository
{
    public sealed class InstrumentationRepository : RepositoryBase<InstrumentationEntity>, IInstrumentationRepository
    {
        private readonly DapperContext dapperContext;
        public InstrumentationRepository(FactDbContext factDbContext, DapperContext dapperContext) : base(factDbContext)
        {
            this.dapperContext = dapperContext;
        }

        public void CreateInstrumentation(InstrumentationEntity createModel)
        {
            try
            {
                Create(createModel);
            }
            catch (Exception ex)
            {
                throw new ArgumentNullException(ex.Message);
            }
        }

        public void DeleteInstrumentation(InstrumentationEntity entity)
        {
            try
            {
                Delete(entity!);
            }
            catch (Exception ex)
            {
                throw new ArgumentNullException(ex.Message);
            }
        }

        public async Task<IEnumerable<InstrumentationDisplayModel>> GetInstrumentationByZoneAsync(int Id)
        {
            try
            {
                using (var connection = dapperContext.CreateConnection())
                {
                    connection.Open();
                    var query = await connection.QueryAsync<InstrumentationDisplayModel>(InstrumentationQuery.GetInstrumentationByZoneSQL, new { ZoneId = Id });
                    connection.Close();
                    return query;
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentNullException(ex.Message);
            }
        }

        public async Task<IEnumerable<InstrumentationEntity>> GetInstrumentationNotInZoneAsync()
        {
            try
            {
                var instrumentations = await FindByCondition(p => p.ZoneId == null, trackChanges: false).ToListAsync();
                return instrumentations ?? Enumerable.Empty<InstrumentationEntity>();
            }
            catch (Exception ex)
            {
                throw new ArgumentNullException(ex.Message);
            }
        }

        public async Task RemoveInstrumentation(int Id)
        {
            var param = new DynamicParameters();
            param.Add("Id", Id);
            using (var connection = dapperContext.CreateConnection())
            {
                connection.Open();
                using (var trans = connection.BeginTransaction())
                {
                    await connection.ExecuteAsync(InstrumentationQuery.ZoneIdToNullSQL, param, transaction: trans);
                    trans.Commit();
                }
                connection.Close();
            }
        }

        public async Task UpdateInforInstrumentation(InstrumentationUpdateModel updateModel)
        {
            var param = new DynamicParameters(updateModel);
            using (var connection = dapperContext.CreateConnection())
            {
                connection.Open();
                using (var trans = connection.BeginTransaction())
                {
                    await connection.ExecuteAsync(InstrumentationQuery.UpdateInfoSQL, param, transaction: trans);
                    trans.Commit();
                }
                connection.Close();
            }
        }
    }
}