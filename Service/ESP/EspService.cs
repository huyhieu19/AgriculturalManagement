using AutoMapper;
using Entities;
using Models;
using Repository.Contracts;
using Service.Contracts;

namespace Service
{
    public class EspService : IEspService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public EspService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<bool> CreateEsp(EspCreateModel model)
        {
            var entity = _mapper.Map<Esp8266Entity>(model);
            _repositoryManager.EspRepository.CreateEsp(entity);
            int isChange = await _repositoryManager.SaveAsync();
            return isChange == 1;
        }

        public async Task<List<EspDisplayModel>> GetAll()
        {
            var entity = await _repositoryManager.EspRepository.GetAll();
            return _mapper.Map<List<EspDisplayModel>>(entity);
        }

    }
}
