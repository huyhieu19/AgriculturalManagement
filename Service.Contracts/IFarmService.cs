using Models;
using Models.Farm;

namespace Service.Contracts
{
    public interface IFarmService
    {
        Task<IEnumerable<FarmDisplayModel>> GetAllFarmAsync(bool trackChanges);
        Task<IEnumerable<FarmDisplayModel>> GetByCondition(QueryBaseModel model, bool trackChanges);
        Task<bool> AddFarm(FarmCreateModel createModel);
        Task<bool> RemoveFarm(int id);
        Task<bool> UpdateFarm(FarmUpdateModel updateModel);
        Task<IEnumerable<FarmFilterNameModel>> GetNameFarm();
    }
}
