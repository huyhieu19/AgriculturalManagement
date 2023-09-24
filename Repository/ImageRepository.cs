using Common.Queries;
using Dapper;
using Database;
using Entities;
using Microsoft.EntityFrameworkCore;
using Models;
using Repository.Contracts;

namespace Repository
{
    public class ImageRepository : RepositoryBase<ImageEntity>, IImageRepository
    {
        private readonly DapperContext dapperContext;

        public ImageRepository(FactDbContext factDbContext, DapperContext dapperContext) : base(factDbContext)
        {
            this.dapperContext = dapperContext;
        }
        public async Task<IEnumerable<ImageEntity>> GetImages(ImageQueryDisplayModel model)
        {
            if (model.IsDefault == true)
            {
                if (model.UserId != null)
                {
                    return await FindByCondition(p => p.UserId == model.UserId && p.IsDefault == true, false).ToListAsync();
                }
                else if (model.FarmId != null)
                {
                    return await FindByCondition(p => p.FarmId == model.FarmId && p.IsDefault == true, false).ToListAsync();
                }
                else if (model.ZoneHarvestId != null)
                {
                    return await FindByCondition(p => p.ZoneHarvestId == model.ZoneHarvestId && p.IsDefault == true, false).ToListAsync();
                }
                else if (model.ZoneId != null)
                {
                    return await FindByCondition(p => p.ZoneId == model.FarmId && p.IsDefault == true, false).ToListAsync();
                }
            }
            else
            {
                if (model.UserId != null)
                {
                    return await FindByCondition(p => p.UserId == model.UserId, false).ToListAsync();
                }
                else if (model.FarmId != null)
                {
                    return await FindByCondition(p => p.FarmId == model.FarmId, false).ToListAsync();
                }
                else if (model.ZoneHarvestId != null)
                {
                    return await FindByCondition(p => p.ZoneHarvestId == model.ZoneHarvestId, false).ToListAsync();
                }
                else if (model.ZoneId != null)
                {
                    return await FindByCondition(p => p.ZoneId == model.FarmId, false).ToListAsync();
                }
            }
            return new List<ImageEntity>() { new ImageEntity() };
        }

        public async Task<bool> SetImage(ImageCreateModel model, string Url)
        {
            var param = new DynamicParameters(model);
            param.Add("Url", Url);

            using (var connection = dapperContext.CreateConnection())
            {
                connection.Open();
                using (var trans = connection.BeginTransaction())
                {
                    await connection.ExecuteAsync(ImageQuery.CreateImage, param, transaction: trans);
                    trans.Commit();
                }
            }
            return true;
        }
    }
}
