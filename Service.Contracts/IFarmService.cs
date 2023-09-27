using Models;

namespace Service.Contracts
{
    public interface IFarmService
    {
        Task<IEnumerable<FarmDisplayModel>> GetAllFarmAsync(bool trackChanges);
        Task<IEnumerable<FarmDisplayModel>> GetByCondition(FarmQueryModel model, bool trackChanges);
        Task<bool> AddFarm(FarmCreateModel createModel);
        Task<bool> RemoveFarm(int id, string UserId);
        Task<bool> UpdateFarm(FarmUpdateModel updateModel);
        Task<IEnumerable<FarmFilterNameModel>> GetNameFarm();
    }
}
