using Models;
using Models.Farm;

namespace Service.Contracts.FarmZone
{
    public interface IFarmService
    {
        Task<List<FarmDisplayModel>> GetFarms(string userId, bool trackChanges);
        Task<FarmModifyResponseModel> AddFarm(FarmCreateModel createModel);
        Task<FarmModifyResponseModel> RemoveFarm(int id, string userId);
        Task<FarmModifyResponseModel> UpdateFarm(FarmUpdateModel updateModel);
    }
}