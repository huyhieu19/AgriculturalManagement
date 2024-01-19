using Common.Enum;
using Models;
using Models.DeviceTimer;
using Models.Farm;

namespace Service.Contracts.FarmZone
{
    public interface IFarmService
    {
        Task<List<FarmDisplayModel>> GetFarms(string userId, bool trackChanges);
        Task<FarmModifyResponseModel> AddFarm(FarmCreateModel createModel);
        Task<FarmModifyResponseModel> RemoveFarm(int id, string userId);
        Task<FarmModifyResponseModel> UpdateFarm(FarmUpdateModel updateModel);

        // Get Farm, Zone, Device for process add timer to device 
        Task<DeviceDriverByFarmDisplayModel> DeviceDriverByFarmZone(string userId, DeviceType deviceType);
    }
}