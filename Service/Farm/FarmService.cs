using AutoMapper;
using Common.Enum;
using Entities.Farm;
using Models;
using Models.DeviceTimer;
using Models.Farm;
using Repository.Contracts;
using Service.Contracts.FarmZone;
using Service.Contracts.Logger;

namespace Service.Farm
{
    public sealed class FarmService : IFarmService
    {
        private readonly IRepositoryManager repositoryManager;
        private readonly ILoggerManager logger;
        private readonly IMapper mapper;
        public FarmService(IRepositoryManager repositoryManager, ILoggerManager logger, IMapper mapper)
        {
            this.repositoryManager = repositoryManager;
            this.logger = logger;
            this.mapper = mapper;
        }

        private async Task<FarmModifyResponseModel> ResponseModel(int isChange, string userId)
        {
            FarmModifyResponseModel responseModel = new FarmModifyResponseModel()
            {
                farmDisplays = await GetFarms(userId, false)
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

        public async Task<FarmModifyResponseModel> AddFarm(FarmCreateModel createModel)
        {
            try
            {
                logger.LogInformation("Create farm in Farms service layer");

                var companyEntity = mapper.Map<FarmEntity>(createModel);
                repositoryManager.Farm.CreateFarm(companyEntity);
                int isChange = await repositoryManager.SaveAsync();
                FarmModifyResponseModel responseModel = await ResponseModel(isChange, createModel.UserId!);
                return responseModel;
            }
            catch (Exception ex)
            {
                throw new AggregateException(ex.Message);
            }
        }

        public async Task<List<FarmDisplayModel>> GetFarms(string userId, bool trackChanges)
        {
            try
            {
                logger.LogInformation($"Farms Service| Get farm by user | start ");
                var farms = await repositoryManager.Farm.GetFarms(userId, trackChanges);
                logger.LogInformation($"Farms Service | Get farm by user | end ");
                return farms;
            }
            catch (Exception ex)
            {
                logger.LogError($"Farms Service | Delete | Exception: {ex}");
                throw;
            }
        }

        public async Task<FarmModifyResponseModel> RemoveFarm(int id, string userId)
        {
            try
            {
                logger.LogInformation($"Farms Service | Remove Farms: {id}");
                repositoryManager.Farm.DeleteFarm(id, userId);
                int isChange = await repositoryManager.SaveAsync();
                FarmModifyResponseModel responseModel = await ResponseModel(isChange, userId);
                return responseModel;
            }
            catch (Exception ex)
            {
                logger.LogError($"Farms Service | Exception: {ex}");
                throw;
            }
        }
        public async Task<FarmModifyResponseModel> UpdateFarm(FarmUpdateModel updateModel)
        {
            try
            {
                logger.LogInformation($"Farms Service | Update Farms: {updateModel}");
                var entity = mapper.Map<FarmEntity>(updateModel);
                repositoryManager.Farm.UpdateFarm(entity);
                int isChange = await repositoryManager.SaveAsync();
                FarmModifyResponseModel responseModel = await ResponseModel(isChange, updateModel.UserId!);
                return responseModel;
            }
            catch (Exception ex)
            {
                logger.LogError($"Farms Service | Exception: {ex}");
                throw;
            }
        }

        public async Task<DeviceDriverByFarmDisplayModel> DeviceDriverByFarmZone(string userId, DeviceType deviceType)
        {
            return await repositoryManager.Farm.DeviceDriverByFarmZone(userId, deviceType);
        }
    }
}