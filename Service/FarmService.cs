using AutoMapper;
using Entities;
using Models.Farm;
using Repository.Contracts;
using Service.Contracts;

namespace Service
{
    public sealed class FarmService : IFarmService
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

        public Task<bool> AddFarm(FarmCreateModel createModel)
        {
            _logger.LogInfomation("Create farm in Farm service layer");
            var companyEntity = _mapper.Map<FarmEntity>(createModel);
            repositoryManager.FarmRepository.CreateFarm(companyEntity);
            return Task.FromResult(true);
        }

        public async Task<IEnumerable<FarmDisplayModel>> GetAllFarmAsync(bool trackChanges)
        {
            _logger.LogInfomation("Get all farms in Service layer");
            var farms = await repositoryManager.FarmRepository.GetAllFarm(trackChanges);
            var farmsDisplayModel = _mapper.Map<IEnumerable<FarmDisplayModel>>(farms);
            return farmsDisplayModel;
        }
    }
}
