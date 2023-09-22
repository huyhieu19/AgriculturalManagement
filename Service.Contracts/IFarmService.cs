using Entities;
using Models.Farm;

namespace Service.Contracts
{
    public interface IFarmService
    {
        Task<IEnumerable<FarmDisplayModel>> GetAllFarmAsync(bool trackChange);
        Task<bool> AddFarm(FarmCreateModel createModel);
    }
}
