using Entities;
using Entities.LogProcess;
using Models;
using Models.DeviceData;
using Models.InstrumentSetThreshold;
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

        Task<List<OnOffDeviceByThresholdModel>> GetValueDeviceForThreshold(IEnumerable<InstrumentationGetForSystem> model);
        // Date
        Task<List<StatisticByDateDisplayModel>> StatisticsByDateDataDevices(StatisticQueryModel model);
        // Hour
        //Task<List<StatisticByDateDisplayModel>> StatisticsByHourDataDevices(Guid DeviceId);


        #region Loging for auto

        Task PushDataLogDeviceOnOff(List<LogDeviceStatusEntity> addModels);
        Task<BaseResModel<LogDeviceStatusEntity>> GetDataLogDeviceOnOff(LogDeviceDataQueryModel queryModel);

        #endregion
    }
}