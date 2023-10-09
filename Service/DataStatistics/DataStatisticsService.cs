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

        public async Task PushDataToDB(InstrumentValueByFiveSecondEntity addModel)
        {
            logger.LogInfomation("Push start");
            await instrumentValue.InsertOneAsync(addModel);
            logger.LogInfomation("Push end");
        }
    }
}
