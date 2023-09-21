using AutoMapper;
using Repository.Contracts;
using Service.Contracts;

namespace Service
{
    public class FarmService : IFarmService
    {
        private readonly IRepositoryManager repositoryManager;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public FarmService(IRepositoryManager repositoryManager, ILoggerManager logger, IMapper mapper)
        {
            this.repositoryManager = repositoryManager;
            _logger = logger;
            _mapper = mapper;
        }

        public Task<IEnumerable<IFarmService>> GetAllFarmAsync(bool trackChange)
        {
            throw new NotImplementedException();
        }
    }
}
