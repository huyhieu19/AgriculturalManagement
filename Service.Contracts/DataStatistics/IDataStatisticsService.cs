using Entities;

namespace Service
{
    public interface IDataStatisticsService
    {
        Task PushDataToDB(InstrumentValueByFiveSecondEntity addModel);
    }
}
