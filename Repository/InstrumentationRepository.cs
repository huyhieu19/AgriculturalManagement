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

        public async Task DeleteInstrumentation(int Id)
        {
            try
            {
                var instrumentation = await FindByCondition(p => p.Id == Id, trackChanges: false).FirstOrDefaultAsync();
                Delete(instrumentation!);
            }
            catch (Exception ex)
            {
                throw new ArgumentNullException(ex.Message);
            }
        }

        public async Task<IEnumerable<InstrumentationEntity>> GetInstrumentationByZoneAsync(int Id)
        {
            try
            {
                var instrumentations = await FindByCondition(p => p.ZoneId == Id, trackChanges: false).ToListAsync();
                return instrumentations ?? Enumerable.Empty<InstrumentationEntity>();
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
                    await connection.ExecuteAsync(InstrumentationQuery.ZoneIdToNull, param, transaction: trans);
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
                    await connection.ExecuteAsync(InstrumentationQuery.UpdateInfo, param, transaction: trans);
                    trans.Commit();
                }
                connection.Close();
            }
        }
    }
}
