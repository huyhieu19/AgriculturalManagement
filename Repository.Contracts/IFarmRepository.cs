using Entities;
using Models;

namespace Repository.Contracts
{
    public interface IFarmRepository
    {
        Task<IEnumerable<FarmEntity>> GetAllFarm(bool trackChanges);
        void CreateFarm(FarmEntity entity);
        void DeleteFarm(int id);
        void UpdateFarm(FarmUpdateModel model);
        Task<IEnumerable<FarmEntity>> GetByCondition(QueryBaseModel model, bool trackchanges);
    }
}
