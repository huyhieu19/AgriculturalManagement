﻿using Common.Queries;
using Dapper;
using Database;
using Entities;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.Zone;
using Repository.Contracts;

namespace Repository
{
    public class ZoneRepository : RepositoryBase<ZoneEntity>, IZoneRepository
    {
        private readonly DapperContext dapperContext;

        public ZoneRepository(FactDbContext factDbContext, DapperContext dapperContext) : base(factDbContext)
        {
            this.dapperContext = dapperContext;
        }

        public void CreateZone(ZoneEntity entity) => Create(entity);

        public void DeleteZone(int id)
        {
            var entity = FindByCondition(p => p.Id == id, false).First();
            if (entity != null)
            {
                Delete(entity);
            }
        }

        public async Task<IEnumerable<ZoneEntity>> GetZones(QueryBaseModel model, bool trackchanges)
        {
            var Zones = await FindAll(trackchanges).ToListAsync();
            if (!string.IsNullOrEmpty(model.SearchTerm))
            {
                string key = model.SearchTerm;
                Zones = Zones.Where(p => p.Name.ToLower().Contains(key.ToLower())).ToList();
            }
            if (model.typeOrderBy is Common.Enum.TypeOrderBy.AToZByName)
            {
                return Zones.OrderBy(p => p.Name).ToList();
            }
            else if (model.typeOrderBy is Common.Enum.TypeOrderBy.ZToAByName)
            {
                return Zones.OrderByDescending(p => p.Name).ToList();
            }
            else if (model.typeOrderBy is Common.Enum.TypeOrderBy.IncreasingDay)
            {
                return Zones.OrderByDescending(p => p.CreateDate).ToList();
            }
            return Zones.OrderBy(p => p.Name).ToList();
        }

        public async void UpdateZone(ZoneUpdateModel model)
        {
            try
            {
                var param = new DynamicParameters(model);

                using (var connection = dapperContext.CreateConnection())
                {
                    connection.Open();
                    using (var trans = connection.BeginTransaction())
                    {
                        await connection.ExecuteAsync(FarmQuery.UpdateFarm, param, transaction: trans);
                        trans.Commit();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
