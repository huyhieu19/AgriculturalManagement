using Database;
using Repository.Contracts;
using Service.Contracts;

namespace Repository
{
    public sealed class RepositoryManager : IRepositoryManager
    {
        private readonly Lazy<IFarmRepository> farmRepository;
        private readonly FactDbContext factDbContext;

        public RepositoryManager(FactDbContext factDbContext, ILoggerManager logger, DapperContext dapperContext)
        {
            this.factDbContext = factDbContext;
            farmRepository = new Lazy<IFarmRepository>(() => new FarmRepository(factDbContext, logger, dapperContext));
        }

        public IFarmRepository FarmRepository => farmRepository.Value;

        public async Task SaveAsync() => await factDbContext.SaveChangesAsync();
    }
}
