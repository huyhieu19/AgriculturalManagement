using Entities.Farm;
using Models;

namespace Repository.Contracts.Farm
{
    public interface IFarmRepository
    {
        void CreateFarm(FarmEntity entity);
        void DeleteFarm(int id, string UserId);
        void UpdateFarm(FarmEntity entity);
        Task<List<FarmDisplayModel>> GetFarms(string userId, bool trackchanges);
    }
}