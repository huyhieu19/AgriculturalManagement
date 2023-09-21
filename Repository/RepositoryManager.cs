using Repository.Contracts;

namespace Repository
{
    public sealed class RepositoryManager : IRepositoryManager
    {
        private readonly Lazy<IFarmRepository> farmRepository;

        public RepositoryManager()
        {
            farmRepository = new Lazy<IFarmRepository>(() => new FarmRepository());
        }

        public IFarmRepository Fram => throw new NotImplementedException();

        public Task SaveAsync()
        {
            throw new NotImplementedException();
        }
    }
}
