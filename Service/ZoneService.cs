using AutoMapper;
using Entities;
using Models.Zone;
using Repository.Contracts;
using Service.Contracts;

namespace Service
{
    public class ZoneService : IZoneService
    {
        private readonly IRepositoryManager repositoryManager;
        private readonly IMapper mapper;

        public ZoneService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            this.repositoryManager = repositoryManager;
            this.mapper = mapper;
        }

        public async Task<bool> AddZone(ZoneCreateModel createModel)
        {
            var entity = mapper.Map<ZoneEntity>(createModel);
            repositoryManager.ZoneRepository.CreateZone(entity);
            await repositoryManager.SaveAsync();
            return true;
        }

        public async Task<IEnumerable<ZoneDisplayModel>> GetZones(ZoneQueryDisplayModel model, bool trackChanges)
        {
            var ZonesModel = await repositoryManager.ZoneRepository.GetZones(model, trackChanges);
            var result = mapper.Map<IEnumerable<ZoneDisplayModel>>(ZonesModel);
            return result;
        }

        public async Task<bool> RemoveZone(int id)
        {
            repositoryManager.ZoneRepository.DeleteZone(id);
            await repositoryManager.SaveAsync();
            return true;
        }

        public async Task<bool> UpdateZone(ZoneUpdateModel updateModel)
        {
            try
            {
                repositoryManager.ZoneRepository.UpdateZone(updateModel);
                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
