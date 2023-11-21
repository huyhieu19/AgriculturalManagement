using Entities;
using Microsoft.Extensions.Options;
using Models;
using Models.Config.Mongo;
using Models.DeviceData;
using Models.LoggerProcess;
using MongoDB.Driver;
using Service.Contracts.Logger;

namespace Service
{
    public class DataStatisticsService : IDataStatisticsService
    {
        private readonly IMongoCollection<InstrumentValueByFiveSecondEntity> instrumentValue;
        private readonly IMongoCollection<LogProcessModel> logProcess;
        private readonly MongoClient client;
        private readonly ILoggerManager logger;

        public DataStatisticsService(IOptions<MongoDbConfigModel> mongoDbConfig, ILoggerManager logger)
        {
            this.client = new MongoClient(mongoDbConfig.Value.ConnectionString);
            var database = this.client.GetDatabase(mongoDbConfig.Value.DatabaseName);
            instrumentValue = database.GetCollection<InstrumentValueByFiveSecondEntity>(mongoDbConfig.Value.CollectionName);
            logProcess = database.GetCollection<LogProcessModel>(mongoDbConfig.Value.CollectionLog);
            this.logger = logger;
        }

        public async Task<BaseResModel<InstrumentValueByFiveSecondEntity>> PullData(DeviceDataQueryModel queryModel)
        {
            var result = await instrumentValue.Find(_ => true).ToListAsync();
            if (queryModel.ValueDate != null)
            {
                result = result.Where(prop => prop.ValueDate!.Value.Date == queryModel.ValueDate.Value.Date).ToList();
            }
            var response = new BaseResModel<InstrumentValueByFiveSecondEntity>()
            {
                Data = result.OrderByDescending(p => p.ValueDate).Skip(queryModel.PageSize * (queryModel.PageNumber - 1)).Take(queryModel.PageSize).ToList(),
                PageNumber = queryModel.PageNumber,
                PageSize = queryModel.PageSize,
                TotalCount = result.Count,
                TotalPage = result.Count / queryModel.PageSize,
            };
            return response;
        }

        public async Task PushMultipleDataToDB(List<InstrumentValueByFiveSecondEntity> addModels)
        {
            try
            {
                logger.LogInformation($"Push multiple start payload");
                await instrumentValue.InsertManyAsync(addModels);
                logger.LogInformation("Push end");
            }
            catch
            {
                logger.LogInformation("Exception PushDatasToDB");
                throw;
            }
        }

        public async Task PushDataToDB(InstrumentValueByFiveSecondEntity addModel)
        {
            logger.LogInformation($"Push start payload: {addModel.PayLoad}");
            await instrumentValue.InsertOneAsync(addModel);
            logger.LogInformation("Push end");
        }

        public async Task WriteLog(LogProcessModel model)
        {
            await logProcess.InsertOneAsync(model);
        }

        public Task StatisticsDay()
        {
            throw new NotImplementedException();
        }

        public Task StatisticsHour()
        {
            throw new NotImplementedException();
        }

        public Task StatisticsMonth()
        {
            throw new NotImplementedException();
        }

        public Task StatisticsWeek()
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResModel<LogProcessModel>> LoggerProcess(LoggerProcessQueryModel queryModel)
        {
            var result = await logProcess.Find(_ => true).ToListAsync();
            if (queryModel.ValueDate != null)
            {
                result = result.Where(p => p.ValueDate!.Value.Date == queryModel.ValueDate.Value.Date).ToList();
            }
            if (queryModel.LoggerProcessType != null)
            {
                result = result.Where(p => p.LoggerProcessType == queryModel.LoggerProcessType.ToString()).ToList();
            }
            if (queryModel.LoggerProcessType != null)
            {
                result = result.Where(p => p.LoggerType == queryModel.LoggerType.ToString()).ToList();
            }
            var response = new BaseResModel<LogProcessModel>()
            {
                Data = result.OrderByDescending(p => p.ValueDate).Skip(queryModel.PageSize * (queryModel.PageNumber - 1)).Take(queryModel.PageSize).ToList(),
                PageNumber = queryModel.PageNumber,
                PageSize = queryModel.PageSize,
                TotalCount = result.Count,
                TotalPage = result.Count / queryModel.PageSize,
            };
            return response;
        }
    }
}