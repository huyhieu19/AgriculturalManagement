using Database;
using Entities;
using Repository.Contracts;

namespace Repository
{
    public class InstrumentationRepository : RepositoryBase<InstrumentationEntity>, IInstrumentationRepository
    {
        public DapperContext dapperContext;
        public InstrumentationRepository(FactDbContext factDbContext, DapperContext dapperContext) : base(factDbContext)
        {
            this.dapperContext = dapperContext;
        }


    }
}
