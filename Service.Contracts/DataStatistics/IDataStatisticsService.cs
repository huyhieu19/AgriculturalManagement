using Entities;

namespace Service
{
    public interface IDataStatisticsService
    {
        Task PushDataToDB(InstrumentValueByFiveSecondEntity addModel);
        Task PushMultipleDataToDB(List<InstrumentValueByFiveSecondEntity> addModels);
        Task GetData();
        Task StatisticsWeek();
        Task StatisticsMonth();
        Task StatisticsDay();
        Task StatisticsHour();
    }
}
