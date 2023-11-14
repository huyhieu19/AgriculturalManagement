using AutoMapper;
using Entities;
using Models;
using Models.Zone;
using Repository.Contracts;
using Service.Contracts.FarmZone;

namespace Service.Farm
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
        private async Task<ZoneModifyResponseModel> ResponseModel(int isChange, int farmId)
        {
            ZoneModifyResponseModel responseModel = new ZoneModifyResponseModel()
            {
                zoneDisplays = await GetZones(farmId, false)
            };

            if (isChange > 0)
            {
                responseModel.isSuccess = true;
            }
            else
            {
                responseModel.isSuccess = false;
            }
            return responseModel;
        }

        public async Task<ZoneModifyResponseModel> AddZone(ZoneCreateModel createModel)
        {
            var entity = mapper.Map<ZoneEntity>(createModel);
            repositoryManager.Zone.CreateZone(entity);
            int isChange = await repositoryManager.SaveAsync();
            var response = await ResponseModel(isChange, createModel.FarmId);
            return response;
        }

        public async Task<List<ZoneDisplayModel>> GetZones(int farmId, bool trackChanges)
        {
            var entities = await repositoryManager.Zone.GetZones(farmId, trackChanges);
            var zones = mapper.Map<List<ZoneDisplayModel>>(entities);
            return zones;
        }

        public async Task<ZoneModifyResponseModel> RemoveZone(int id, int farmId)
        {
            repositoryManager.Zone.DeleteZone(id, farmId);
            var isChange = await repositoryManager.SaveAsync();
            var response = await ResponseModel(isChange, farmId);
            return response;
        }

        public async Task<ZoneModifyResponseModel> UpdateZone(ZoneUpdateModel updateModel)
        {
            try
            {
                var entity = mapper.Map<ZoneEntity>(updateModel);
                repositoryManager.Zone.UpdateZone(entity);
                int isChange = await repositoryManager.SaveAsync();
                var response = await ResponseModel(isChange, updateModel.FarmId);
                return response;
            }
            catch (Exception ex)
            {
                throw new AggregateException(ex.Message);
            }
        }
    }
}