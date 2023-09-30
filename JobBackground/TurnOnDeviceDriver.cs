using Common.Queries;
using Dapper;
using Database;
using Models;
using Quartz;

namespace JobBackground
{
    internal class TurnOnDeviceDriver : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            var list = await GetList(context);
        }

        public async Task<IEnumerable<DeviceDriverTurnOnTurnOffModel>> GetList(IJobExecutionContext context)
        {
            // Lấy DapperContext từ JobDataMap
            var jobDataMap = context.MergedJobDataMap;
            var dapperContext = (DapperContext)jobDataMap["dapperContext"];

            var query = TimerDeviceDriverQuery.GetAllTimerSQL;
            IEnumerable<DeviceDriverTurnOnTurnOffModel> listTime;
            using (var connection = dapperContext.CreateConnection())
            {
                listTime = await connection.QueryAsync<DeviceDriverTurnOnTurnOffModel>(query);
            }
            return listTime;
        }
    }
}
