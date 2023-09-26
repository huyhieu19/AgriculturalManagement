using Database;
using Entities;
using Models;
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

        public Task<bool> CreateTypeInstrumentations(List<InstrumentationTypeDisplayModel> model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteTypeInstrumentations(List<int> Ids)
        {
            throw new NotImplementedException();
        }

        public Task<List<InstrumentationTypeEntity>> GetTypeInstrumentation()
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateTypeInstrumentation(InstrumentationTypeDisplayModel model)
        {
            throw new NotImplementedException();
        }

    }
}
