using Entities;
using Models;

namespace Repository.Contracts
{
    public interface IFarmRepository
    {
        Task<IEnumerable<FarmEntity>> GetAllFarm(bool trackChanges);
        void CreateFarm(FarmEntity entity);
        void DeleteFarm(int id, string UserId);
        void UpdateFarm(FarmEntity entity);
        Task<IEnumerable<FarmDisplayModel>> GetFarms(string userId, bool trackchanges);

    }
}