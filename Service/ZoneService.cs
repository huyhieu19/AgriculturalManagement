using AutoMapper;
using Entities;
using Models;
using Repository.Contracts;
using Service.Contracts;

namespace Service
{
    public sealed class ZoneService : IZoneService
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
            repositoryManager.Zone.CreateZone(entity);
            await repositoryManager.SaveAsync();
            return true;
        }

        public async Task<IEnumerable<ZoneDisplayModel>> GetZones(int farmId, bool trackChanges)
        {
            var ZonesModel = await repositoryManager.Zone.GetZones(farmId, trackChanges);
            var result = mapper.Map<IEnumerable<ZoneDisplayModel>>(ZonesModel);
            return result;
        }

        public async Task<bool> RemoveZone(int id)
        {
            repositoryManager.Zone.DeleteZone(id);
            await repositoryManager.SaveAsync();
            return true;
        }

        public async Task<bool> UpdateZone(ZoneUpdateModel updateModel)
        {
            try
            {
                repositoryManager.Zone.UpdateZone(updateModel);
                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                throw new AggregateException(ex.Message);
            }
        }
    }
}
