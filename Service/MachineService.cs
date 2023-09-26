using AutoMapper;
using Repository.Contracts;
using Service.Contracts;

namespace Service
{
    public sealed class MachineService : IMachineService
    {
        private readonly IRepositoryManager repositoryManager;
        private readonly IMapper mapper;

        public MachineService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            this.repositoryManager = repositoryManager;
            this.mapper = mapper;
        }
    }
}
