using Database;
using Entities;
using Repository.Contracts;

namespace Repository
{
    public sealed class MachineRepository : RepositoryBase<MachineEntity>, IMachineRepository
    {
        private readonly DapperContext dapperContext;
        public MachineRepository(FactDbContext factDbContext, DapperContext dapperContext) : base(factDbContext)
        {
            this.dapperContext = dapperContext;
        }
    }
}
