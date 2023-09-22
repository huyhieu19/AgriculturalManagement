using Entities;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Contracts
{
    public interface IFarmRepository
    {
        Task<IEnumerable<FarmEntity>> GetAllFarm(bool trackChanges);
        Task CreateFarm(FarmEntity entity);
        Task DeleteFarm(int id);
        Task UpdateFarm(FarmEntity entity);
        Task<IEnumerable<FarmEntity>> GetByCondition(QueryBaseModel model, bool trackchanges);
    }
}
