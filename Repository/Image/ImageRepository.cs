using Common.Queries;
using Dapper;
using Database;
using Entities.Image;
using Microsoft.EntityFrameworkCore;
using Models;
using Repository.Contracts.Image;

namespace Repository.Image
{
    public sealed class ImageRepository : RepositoryBase<ImageEntity>, IImageRepository
    {
        private readonly DapperContext dapperContext;

        public ImageRepository(FactDbContext factDbContext, DapperContext dapperContext) : base(factDbContext)
        {
            this.dapperContext = dapperContext;
        }

        public async Task<bool> DeleteImage(int Id)
        {
            var image = await FindByCondition(p => p.Id == Id, trackChanges: false).FirstOrDefaultAsync();
            Delete(image!);
            return true;
        }

        public async Task<IEnumerable<ImageEntity>> GetImages(ImageQueryDisplayModel model)
        {
            if (model.IsDefault == true)
            {
                if (model.FarmId != null)
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
                else if (model.DeviceDriverId != null)
                {
                    return await FindByCondition(p => p.DeviceDriverId == model.DeviceDriverId && p.IsDefault == true, false).ToListAsync();
                }
                else if (model.InstrumentationId != null)
                {
                    return await FindByCondition(p => p.InstrumentationId == model.InstrumentationId && p.IsDefault == true, false).ToListAsync();
                }
            }
            else
            {
                if (model.FarmId != null)
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
                if (model.DeviceDriverId != null)
                {
                    return await FindByCondition(p => p.DeviceDriverId == model.DeviceDriverId, false).ToListAsync();
                }
                else if (model.InstrumentationId != null)
                {
                    return await FindByCondition(p => p.InstrumentationId == model.InstrumentationId, false).ToListAsync();
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
                    if (model.IsDefault == true)
                    {
                        await connection.ExecuteAsync(ImageQuery.SetAllDefaultToFalseSQL, transaction: trans);
                    }
                    await connection.ExecuteAsync(ImageQuery.CreateImageSQL, param, transaction: trans);
                    trans.Commit();
                }
                connection.Close();
            }
            return true;
        }

        public async Task SetImageDefault(int Id)
        {
            var param = new DynamicParameters();
            param.Add("Id", Id);

            using (var connection = dapperContext.CreateConnection())
            {
                connection.Open();
                using (var trans = connection.BeginTransaction())
                {
                    await connection.ExecuteAsync(ImageQuery.SetImageDefaultSQl, param, transaction: trans);
                    trans.Commit();
                }
            }
        }
    }
}