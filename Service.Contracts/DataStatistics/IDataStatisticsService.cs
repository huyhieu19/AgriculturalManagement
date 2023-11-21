using Entities;
using Models;
using Models.DeviceData;
using Models.LoggerProcess;

namespace Service
{
    public interface IDataStatisticsService
    {
        Task PushDataToDB(InstrumentValueByFiveSecondEntity addModel);
        Task PushMultipleDataToDB(List<InstrumentValueByFiveSecondEntity> addModels);
        Task<BaseResModel<InstrumentValueByFiveSecondEntity>> PullData(DeviceDataQueryModel queryModel);
        Task<BaseResModel<LogProcessModel>> LoggerProcess(LoggerProcessQueryModel queryModel);
        Task WriteLog(LogProcessModel model);
        Task StatisticsWeek();
        Task StatisticsMonth();
        Task StatisticsDay();
        Task StatisticsHour();
    }
}