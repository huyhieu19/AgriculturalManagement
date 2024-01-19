using Common.Enum;
using Dapper;
using Database;
using Entities;
using Entities.LogProcess;
using EnumsNET;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Models;
using Models.Config.Mongo;
using Models.DeviceData;
using Models.InstrumentSetThreshold;
using Models.LoggerProcess;
using Models.Statistic;
using MongoDB.Driver;
using Service.Contracts.Logger;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Service
{
    public sealed class DataStatisticsService : IDataStatisticsService
    {
        private readonly IMongoCollection<InstrumentValueByFiveSecondEntity> instrumentValue;
        private readonly IMongoCollection<LogProcessEntity> logProcess;
        private readonly IMongoCollection<LogDeviceStatusEntity> logDevice;
        private readonly MongoClient client;
        private readonly ILoggerManager logger;
        private readonly DapperContext dapperContext;


        public DataStatisticsService(IOptions<MongoDbConfigModel> mongoDbConfig, ILoggerManager logger, DapperContext dapper)
        {
            this.client = new MongoClient(mongoDbConfig.Value.ConnectionString);
            var database = this.client.GetDatabase(mongoDbConfig.Value.DatabaseName);
            instrumentValue = database.GetCollection<InstrumentValueByFiveSecondEntity>(mongoDbConfig.Value.CollectionName);
            logProcess = database.GetCollection<LogProcessEntity>(mongoDbConfig.Value.CollectionLog);
            logDevice = database.GetCollection<LogDeviceStatusEntity>(mongoDbConfig.Value.CollectionLogDevice);
            this.logger = logger;
            this.dapperContext = dapper;
        }

        public async Task<BaseResModel<InstrumentValueByFiveSecondEntity>> PullData(DeviceDataQueryModel queryModel)
        {
            var result = await instrumentValue.Find(_ => true).ToListAsync();
            if (queryModel.ValueDate != null)
            {
                result = result.Where(prop => prop.ValueDate!.Date == queryModel.ValueDate.Value.Date).ToList();
            }
            var response = new BaseResModel<InstrumentValueByFiveSecondEntity>()
            {
                Data = result.OrderByDescending(p => p.ValueDate).Skip(queryModel.PageSize * (queryModel.PageNumber - 1)).Take(queryModel.PageSize).ToList(),
                PageNumber = queryModel.PageNumber,
                PageSize = queryModel.PageSize,
                TotalCount = result.Count,
                TotalPage = result.Count / queryModel.PageSize,
            };
            return response;
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

        public async Task WriteLog(LogProcessEntity model)
        {
            await logProcess.InsertOneAsync(model);
        }


        public async Task<BaseResModel<LogProcessEntity>> LoggerProcess(LoggerProcessQueryModel queryModel)
        {
            var result = await logProcess.Find(_ => true).ToListAsync();
            if (queryModel.ValueDate != null)
            {
                result = result.Where(p => p.ValueDate!.Value.Date == queryModel.ValueDate.Value.Date).ToList();
            }
            if (queryModel.LoggerProcessType != null)
            {
                result = result.Where(p => p.LoggerProcessType == queryModel.LoggerProcessType.ToString()).ToList();
            }
            if (queryModel.LoggerProcessType != null)
            {
                result = result.Where(p => p.LoggerType == queryModel.LoggerType.ToString()).ToList();
            }
            var response = new BaseResModel<LogProcessEntity>()
            {
                Data = result.OrderByDescending(p => p.ValueDate).Skip(queryModel.PageSize * (queryModel.PageNumber - 1)).Take(queryModel.PageSize).ToList(),
                PageNumber = queryModel.PageNumber,
                PageSize = queryModel.PageSize,
                TotalCount = result.Count,
                TotalPage = result.Count / queryModel.PageSize,
            };
            return response;
        }
        private class DeviceTypeGet
        {
            public FunctionDeviceType NameRef { get; set; }
            public string DeviceType { get; set; } = string.Empty;
            public StatisticType TypeStatis { get; set; }
        }

        // By Date
        public async Task<List<StatisticByDateDisplayModel>> StatisticsByDateDataDevices(StatisticQueryModel model)
        {
            StatisticType type;
            FunctionDeviceType nameRef = FunctionDeviceType.None;
            string deviceType = "";

            using (var connection = dapperContext.CreateConnection())
            {
                connection.Open();

                // Replace "YourQueryHere" with your SQL query or stored procedure
                string query = "SELECT TypeStatis,DeviceType, NameRef  FROM [Device] WHERE Id = @DeviceId";

                // Replace YourModelType with the actual type of your model
                var result = await connection.QueryAsync<DeviceTypeGet>(query, new { DeviceId = model.DeviceId });

                var typeObject = result.FirstOrDefault();

                if (typeObject != null)
                {
                    type = typeObject.TypeStatis;
                    nameRef = typeObject.NameRef;
                    deviceType = typeObject.DeviceType;
                }
                else
                {
                    // Handle the case where no result is found, set a default value or throw an exception as needed
                    type = StatisticType.ValueDouble; // Replace Default with your actual default value
                }
            }
            switch (type)
            {
                case StatisticType.ValueDouble:
                    return await StatisticsByDateDataDevicesValueDouble(model.DeviceId, nameRef, deviceType, model.ValueDate);
                case StatisticType.ValueOnOff:
                    return await StatisticsByDateDataDevicesValueOnOff(model.DeviceId, nameRef, deviceType);
                case StatisticType.ValueDetect:
                    return await StatisticsByDateDataDevicesValueDouble(model.DeviceId, nameRef, deviceType, model.ValueDate);
            }
            return await StatisticsByDateDataDevicesValueDouble(model.DeviceId, nameRef, deviceType, model.ValueDate);
        }
        private async Task<List<StatisticByDateDisplayModel>> StatisticsByDateDataDevicesValueDouble(Guid DeviceId, FunctionDeviceType NameRef, string DeviceType, DateTime ValueDate)
        {
            string Nameref = ((FunctionDeviceType)NameRef).AsString(EnumFormat.Description)!;
            var result = await instrumentValue.Find(p => p.DeviceId!.Equals(DeviceId.ToString(), StringComparison.OrdinalIgnoreCase) && p.DeviceType == Nameref && p.ValueDate.Date == ValueDate.Date && p.PayLoad != "nan")
            .ToListAsync();
            List<StatisticByDateDisplayModel> data = new List<StatisticByDateDisplayModel>();

            var result1 = result.Where(p => p.DeviceNumber == Nameref).ToList();
            var data1 = Enumerable.Range(0, 24)
           .GroupJoin(result1,
               hour => hour,
               item => item.ValueDate.Hour,
               (hour, group) => new
               {
                   Hour = hour,
                   Values = group.Select(item => double.TryParse(item.PayLoad, out double payloadValue) ? payloadValue : 0).ToList()
               })
           .Select(group =>
           {
               var values = group.Values;
               return new StatisticByDateDisplayModel
               {
                   NameDevice = DeviceId.ToString(),
                   Type = StatisticType.ValueDouble,
                   ValueAVG = values.Any() ? values.Average() : 0,
                   ValueMAX = values.Any() ? values.Max() : 0,
                   ValueMIN = values.Any() ? values.Min() : 0,
                   CountTotal = values.Count,
                   ValueDate = ValueDate.AddHours(group.Hour)
               };
           })
           .OrderBy(item => item.ValueDate)
           .ToList();
            data.AddRange(data1);

            // Sử dụng JsonNumberHandling.AllowNamedFloatingPointLiterals
            var jsonSerializerOptions = new JsonSerializerOptions
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals,
                WriteIndented = true
            };

            var jsonData = JsonSerializer.Serialize(data, jsonSerializerOptions);
            return data;
        }

        private async Task<List<StatisticByDateDisplayModel>> StatisticsByDateDataDevicesValueOnOff(Guid DeviceId, FunctionDeviceType NameRef, string DeviceType)
        {
            string Nameref = ((FunctionDeviceType)NameRef).AsString(EnumFormat.Description)!;
            var result = await instrumentValue.Find(p => p.DeviceId == DeviceId.ToString() && p.DeviceNumber == Nameref && p.DeviceType == DeviceType).ToListAsync();

            var data = result
           .GroupBy(item => new { item.DeviceId, item.ValueDate.Date })
           .Select(group =>
           {
               var values = group.Select(item => double.Parse(item.PayLoad ?? "_")).ToList();
               return new StatisticByDateDisplayModel
               {
                   NameDevice = group.Key.DeviceId,
                   Type = StatisticType.ValueDouble,  // Đặt kiểu thống kê tương ứng
                   CountOn = group.Count(e => e.PayLoad == "1"),
                   CountOff = group.Count(e => e.PayLoad == "0"),
                   CountTotal = group.Count(),
                   ValueDate = group.Key.Date
               };
           })
           .ToList();

            return data;
        }


        private async Task<List<StatisticByDateDisplayModel>> StatisticsByHourDataDevicesValueDouble(Guid DeviceId, string NameRef, string DeviceType)
        {
            var result = await instrumentValue.Find(p => p.DeviceId == DeviceId.ToString() && p.DeviceNumber == NameRef && p.DeviceType == DeviceType)
            .ToListAsync();

            var data = Enumerable.Range(0, 24)
                .GroupJoin(result,
                    hour => hour,
                    item => item.ValueDate.Hour,
                    (hour, group) => new
                    {
                        Hour = hour,
                        Values = group.Select(item => double.Parse(item.PayLoad ?? "0")).ToList()
                    })
                .Select(group =>
                {
                    var values = group.Values;
                    return new StatisticByDateDisplayModel
                    {
                        NameDevice = DeviceId.ToString(), // Thiết lập tên thiết bị (bạn có thể muốn cập nhật theo yêu cầu của mình)
                        Type = StatisticType.ValueDouble,
                        ValueAVG = values.Any() ? values.Average() : 0,
                        ValueMAX = values.Any() ? values.Max() : 0,
                        ValueMIN = values.Any() ? values.Min() : 0,
                        CountTotal = values.Count,
                        ValueDate = DateTime.Today.AddHours(group.Hour)
                    };
                })
                .OrderBy(item => item.ValueDate)
                .ToList();

            return data;
        }
        private async Task<List<StatisticByDateDisplayModel>> StatisticsByHourDataDevicesValueOnOff(Guid DeviceId, string NameRef, string DeviceType)
        {
            var result = await instrumentValue.Find(p => p.DeviceId == DeviceId.ToString() && p.DeviceNumber == NameRef && p.DeviceType == DeviceType).ToListAsync();

            var data = result
           .GroupBy(item => new { item.DeviceId, item.ValueDate.Hour, item.ValueDate.Date })
           .Select(group =>
           {
               var values = group.Select(item => double.Parse(item.PayLoad ?? "_")).ToList();
               return new StatisticByDateDisplayModel
               {
                   NameDevice = group.Key.DeviceId,
                   Type = StatisticType.ValueDouble,  // Đặt kiểu thống kê tương ứng
                   CountOn = group.Count(e => e.PayLoad == "1"),
                   CountOff = group.Count(e => e.PayLoad == "0"),
                   CountTotal = group.Count(),
                   ValueDate = new DateTime(group.Key.Date.Year, group.Key.Date.Month, group.Key.Date.Day, group.Key.Hour, 0, 0),
               };
           })
           .ToList();

            return data;
        }
        #region Log Device ONOFF
        public async Task PushDataLogDeviceOnOff(List<LogDeviceStatusEntity> addModels)
        {
            await logDevice.InsertManyAsync(addModels);
        }

        public async Task<BaseResModel<LogDeviceStatusEntity>> GetDataLogDeviceOnOff(LogDeviceDataQueryModel queryModel)
        {
            var result = await logDevice.Find(_ => true).ToListAsync();
            if (queryModel.ValueDate != null)
            {
                result = result.Where(prop => prop.ValueDate!.Value.Date == queryModel.ValueDate.Value.Date).ToList();
            }
            if (queryModel.TypeOnOff != null)
            {
                result = result.Where(prop => prop.TypeOnOff == (int)queryModel.TypeOnOff).ToList();
            }
            var response = new BaseResModel<LogDeviceStatusEntity>()
            {
                Data = result.OrderByDescending(p => p.ValueDate).Skip(queryModel.PageSize * (queryModel.PageNumber - 1)).Take(queryModel.PageSize).ToList(),
                PageNumber = queryModel.PageNumber,
                PageSize = queryModel.PageSize,
                TotalCount = result.Count,
                TotalPage = result.Count / queryModel.PageSize,
            };
            return response;
        }
        #endregion

        public async Task<List<OnOffDeviceByThresholdModel>> GetValueDeviceForThreshold(IEnumerable<InstrumentationGetForSystem> model)
        {
            var result = new List<OnOffDeviceByThresholdModel>();
            foreach (var item in model)
            {
                //logger.LogInformation("Time: " + DateTime.UtcNow.AddHours(+7).Round(TimeSpan.FromMinutes(1)));

                var Value = await instrumentValue.Find(p => p.DeviceId!.ToLower() == item.InstrumentationId.ToString().ToLower()
                //&& DateTime.UtcNow.AddHours(+7).Round(TimeSpan.FromMinutes(1)) == p.ValueDate.Date.Round(TimeSpan.FromMinutes(1))
                ).SortByDescending(p => p.ValueDate).FirstOrDefaultAsync();

                if (Value != null)
                {
                    int value;
                    bool successT = int.TryParse(Value.PayLoad, out value);
                    if (successT)
                    {
                        if (item.OnInUpperThreshold)
                        {
                            if (item.ThresholdValueOn < value && !item.DeviceDriverAction)
                            {

                                // Logic mở thiết bị điều khiển
                                var OnOff = new OnOffDeviceByThresholdModel()
                                {
                                    DeviceId = item.DeviceDriverId,
                                    DeviceName = item.NameDeviceDriver,
                                    DeviceType = DeviceType.W,
                                    ModuleId = item.ModuleDriverId,
                                    RequestOn = true,
                                    ThresholdId = item.Id,
                                };
                                result.Add(OnOff);
                            }
                            else if (item.ThresholdValueOff > value && item.DeviceDriverAction)
                            {
                                // Logic đóng thiết bị điều khiển
                                var OnOff = new OnOffDeviceByThresholdModel()
                                {
                                    DeviceId = item.DeviceDriverId,
                                    DeviceName = item.NameDeviceDriver,
                                    DeviceType = DeviceType.W,
                                    ModuleId = item.ModuleDriverId,
                                    RequestOn = false,
                                    ThresholdId = item.Id,
                                };
                                result.Add(OnOff);
                            }
                        }
                        else
                        {
                            if (item.ThresholdValueOn < value && item.DeviceDriverAction)
                            {
                                // Logic đóng thiết bị điều khiển 
                                var OnOff = new OnOffDeviceByThresholdModel()
                                {
                                    DeviceId = item.DeviceDriverId,
                                    DeviceName = item.NameDeviceDriver,
                                    DeviceType = DeviceType.W,
                                    ModuleId = item.ModuleDriverId,
                                    RequestOn = false,
                                    ThresholdId = item.Id,
                                };
                                result.Add(OnOff);
                            }
                            else if (item.ThresholdValueOff > value && !item.DeviceDriverAction)
                            {
                                // Logic mở thiết bị điều khiển
                                var OnOff = new OnOffDeviceByThresholdModel()
                                {
                                    DeviceId = item.DeviceDriverId,
                                    DeviceName = item.NameDeviceDriver,
                                    DeviceType = DeviceType.W,
                                    ModuleId = item.ModuleDriverId,
                                    RequestOn = true,
                                    ThresholdId = item.Id,
                                };
                                result.Add(OnOff);
                            }
                        }
                    }
                }
            }
            return result;
        }
    }
}