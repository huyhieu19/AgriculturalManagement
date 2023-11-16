using Database;
using Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Contracts;

namespace Repository
{
    public sealed class InstrumentationTypeRepository : RepositoryBase<InstrumentationTypeEntity>, IInstrumentationTypeRepository
    {
        private readonly DapperContext dapperContext;
        public InstrumentationTypeRepository(FactDbContext factDbContext, DapperContext dapperContext) : base(factDbContext)
        {
            this.dapperContext = dapperContext;
        }

        public void CreateTypeInstrumentations(InstrumentationTypeEntity entity)
        {
            Create(entity);
        }

        public void DeleteTypeInstrumentations(InstrumentationTypeEntity entity)
        {
            Delete(entity);
        }

        public async Task<List<InstrumentationTypeEntity>> GetTypeInstrumentation()
        {
            return await FindAll(trackChanges: false).ToListAsync();
        }

        public void UpdateTypeInstrumentation(InstrumentationTypeEntity entity)
        {
            Update(entity);
        }

    }
}