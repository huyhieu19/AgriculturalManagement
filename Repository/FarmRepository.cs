using Database;
using Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class FarmRepository : RepositoryBase<FarmEntity>, IFarmRepository
    {
        public FarmRepository(FactDbContext factDbContext) : base(factDbContext)
        {
        }

        public Task CreateFarm(FarmEntity entity)
        {
            Create(entity);
            FactDbContext.SaveChanges();
            return Task.CompletedTask;
        }

        public async Task<IEnumerable<FarmEntity>> GetAllFarm(bool trackChanges) => await FindAll(trackChanges).OrderBy(c => c.Name).ToListAsync();
    }
}
