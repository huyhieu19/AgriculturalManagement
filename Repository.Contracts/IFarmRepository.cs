using Entities;
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
    }
}
