using Entities;
using Microsoft.Extensions.Options;
using Models;
using MongoDB.Driver;
using Service.Contracts;

namespace Service
{
    public class DataStatisticsService : IDataStatisticsService
    {
        private readonly IMongoCollection<InstrumentValueByFiveSecondEntity> instrumentValue;
        private readonly MongoClient client;
        private readonly ILoggerManager logger;

        public DataStatisticsService(IOptions<MongoDbConfig> mongoDbConfig, ILoggerManager logger)
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

        public async Task PushDatasToDB(List<InstrumentValueByFiveSecondEntity> addModel)
        {
            try
            {
                logger.LogInfomation($"Push multiple start payload");
                await instrumentValue.InsertManyAsync(addModel);
                logger.LogInfomation("Push end");
            }
            catch
            {
                logger.LogInfomation("Exception PushDatasToDB");
                throw;
            }
        }

        public async Task PushDataToDB(InstrumentValueByFiveSecondEntity addModel)
        {
            logger.LogInfomation($"Push start payload: {addModel.PayLoad}, topic: {addModel.Topic}");
            await instrumentValue.InsertOneAsync(addModel);
            logger.LogInfomation("Push end");
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
