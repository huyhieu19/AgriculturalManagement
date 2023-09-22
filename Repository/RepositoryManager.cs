using Database;
using Repository.Contracts;

namespace Repository
{
    public sealed class RepositoryManager : IRepositoryManager
    {
        private readonly Lazy<IFarmRepository> farmRepository;
        private readonly FactDbContext factDbContext;

        public RepositoryManager(FactDbContext factDbContext)
        {
            this.factDbContext = factDbContext;
            farmRepository = new Lazy<IFarmRepository>(() => new FarmRepository(factDbContext));
        }

        public IFarmRepository FarmRepository => farmRepository.Value;

        public async Task SaveAsync() => await factDbContext.SaveChangesAsync();
    }
}
