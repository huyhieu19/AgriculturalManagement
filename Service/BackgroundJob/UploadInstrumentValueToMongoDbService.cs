using Entities;
using Microsoft.Extensions.Hosting;
using Service.Contracts;

namespace Service
{
    public class UploadInstrumentValueToMongoDbService : BackgroundService
    {
        private readonly IDataStatisticsService dataStatisticsService;

        public UploadInstrumentValueToMongoDbService(IDataStatisticsService dataStatisticsService)
        {
            this.dataStatisticsService = dataStatisticsService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // Run every 5 seconds
            while (!stoppingToken.IsCancellationRequested)
            {

                await dataStatisticsService.PushDataToDB(new InstrumentValueByFiveSecondEntity()
                {
                    PayLoad = $"abc {DateTime.Now}",
                    Topic = $"{new Random().NextInt64(1, 100)}",
                    ValueDate = DateTime.Now,
                });
                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            }
        }
    }
}
