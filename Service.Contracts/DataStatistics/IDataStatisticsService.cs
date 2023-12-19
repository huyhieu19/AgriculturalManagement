using Entities;
using Models;
using Models.DeviceData;
using Models.LoggerProcess;
using Models.Statistic;

namespace Service
{
    public interface IDataStatisticsService
    {
        Task PushDataToDB(InstrumentValueByFiveSecondEntity addModel);
        Task PushMultipleDataToDB(List<InstrumentValueByFiveSecondEntity> addModels);
        Task<BaseResModel<InstrumentValueByFiveSecondEntity>> PullData(DeviceDataQueryModel queryModel);
        Task<BaseResModel<LogProcessEntity>> LoggerProcess(LoggerProcessQueryModel queryModel);
        Task WriteLog(LogProcessEntity model);

        // Date
        Task<List<StatisticDisplayModel>> StatisticsByDateDataDevices(Guid DeviceId);
        // Hour
        Task<List<StatisticDisplayModel>> StatisticsByHourDataDevices(Guid DeviceId);
    }
}