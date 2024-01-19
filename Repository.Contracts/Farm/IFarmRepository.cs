using Common.Enum;
using Entities.Farm;
using Models;
using Models.DeviceTimer;

namespace Repository.Contracts.Farm
{
    public interface IFarmRepository
    {
        void CreateFarm(FarmEntity entity);
        void DeleteFarm(int id, string UserId);
        void UpdateFarm(FarmEntity entity);
        Task<List<FarmDisplayModel>> GetFarms(string userId, bool trackchanges);

        // Get Farm, Zone, Device for process add timer to device 
        Task<DeviceDriverByFarmDisplayModel> DeviceDriverByFarmZone(string userId, DeviceType deviceType);
    }
}