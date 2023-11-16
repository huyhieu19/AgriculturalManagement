using Entities;
using Microsoft.Extensions.Options;
using Models.Config.Mongo;
using MongoDB.Driver;
using Service.Contracts.Logger;

namespace Service
{
    public class DataStatisticsService : IDataStatisticsService
    {
        private readonly IMongoCollection<InstrumentValueByFiveSecondEntity> instrumentValue;
        private readonly MongoClient client;
        private readonly ILoggerManager logger;

        public DataStatisticsService(IOptions<MongoDbConfigModel> mongoDbConfig, ILoggerManager logger)
        {
            this.client = new MongoClient(mongoDbConfig.Value.ConnectionString);
            var database = this.client.GetDatabase(mongoDbConfig.Value.DatabaseName);
            instrumentValue = database.GetCollection<InstrumentValueByFiveSecondEntity>(mongoDbConfig.Value.CollectionName);
            this.logger = logger;
        }

        public Task GetData()
        {
            throw new NotImplementedException();
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
    }
}