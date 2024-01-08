using Common.Enum;
using Dapper;
using Database;
using Entities;
using Entities.LogProcess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Models;
using Models.Config.Mongo;
using Models.DeviceData;
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
                result = result.Where(prop => prop.ValueDate!.Value.Date == queryModel.ValueDate.Value.Date).ToList();
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
            public string NameRef { get; set; } = string.Empty;
            public string DeviceType { get; set; } = string.Empty;
            public StatisticType TypeStatis { get; set; }
        }

        // By Date
        public async Task<List<StatisticByDateDisplayModel>> StatisticsByDateDataDevices(StatisticQueryModel model)
        {
            StatisticType type;
            string nameRef = "";
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
        private async Task<List<StatisticByDateDisplayModel>> StatisticsByDateDataDevicesValueDouble(Guid DeviceId, string NameRef, string DeviceType, DateTime ValueDate)
        {
            var result = await instrumentValue.Find(p => p.DeviceId!.Equals(DeviceId.ToString(), StringComparison.OrdinalIgnoreCase) && p.DeviceType == NameRef && p.ValueDate!.Value.Date == ValueDate.Date && p.PayLoad != "nan")
            .ToListAsync();
            List<StatisticByDateDisplayModel> data = new List<StatisticByDateDisplayModel>();
            var nameParts = NameRef.Split('_');

            for (int i = 0; i < nameParts.Length; i++)
            {
                var result1 = result.Where(p => p.DeviceNumber == nameParts[i]).ToList();
                var data1 = Enumerable.Range(0, 24)
               .GroupJoin(result1,
                   hour => hour,
                   item => item.ValueDate?.Hour ?? 0,
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
            }
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

        private async Task<List<StatisticByDateDisplayModel>> StatisticsByDateDataDevicesValueOnOff(Guid DeviceId, string NameRef, string DeviceType)
        {
            var result = await instrumentValue.Find(p => p.DeviceId == DeviceId.ToString() && p.DeviceNumber == NameRef && p.DeviceType == DeviceType).ToListAsync();

            var data = result
           .GroupBy(item => new { item.DeviceId, item.ValueDate?.Date })
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
                   ValueDate = group.Key.Date.Value
               };
           })
           .ToList();

            return data;
        }

        // Statistic By Hour
        //public async Task<List<StatisticByDateDisplayModel>> StatisticsByHourDataDevices(Guid DeviceId)
        //{
        //    StatisticType type;
        //    string nameRef = "";
        //    string deviceType = "";

        //    using (var connection = dapperContext.CreateConnection())
        //    {
        //        connection.Open();

        //        // Replace "YourQueryHere" with your SQL query or stored procedure
        //        string query = "SELECT TypeStatis FROM [Device] WHERE Id = @DeviceId";

        //        // Replace YourModelType with the actual type of your model
        //        var result = await connection.QueryAsync<DeviceTypeGet>(query, new { DeviceId = DeviceId });

        //        var typeObject = result.FirstOrDefault();

        //        if (typeObject != null)
        //        {
        //            type = typeObject.TypeStatis;
        //            nameRef = typeObject.NameRef;
        //            deviceType = typeObject.DeviceType;
        //        }
        //        else
        //        {
        //            // Handle the case where no result is found, set a default value or throw an exception as needed
        //            type = StatisticType.ValueDouble; // Replace Default with your actual default value
        //        }
        //    }
        //    switch (type)
        //    {
        //        case StatisticType.ValueDouble:
        //            return await StatisticsByHourDataDevicesValueDouble(DeviceId, nameRef, deviceType);
        //        case StatisticType.ValueOnOff:
        //            return await StatisticsByHourDataDevicesValueOnOff(DeviceId, nameRef, deviceType);
        //        case StatisticType.ValueDetect:
        //            return await StatisticsByDateDataDevicesValueDouble(DeviceId, nameRef, deviceType);
        //    }
        //    return await StatisticsByHourDataDevicesValueDouble(DeviceId, nameRef, deviceType);
        //}

        private async Task<List<StatisticByDateDisplayModel>> StatisticsByHourDataDevicesValueDouble(Guid DeviceId, string NameRef, string DeviceType)
        {
            var result = await instrumentValue.Find(p => p.DeviceId == DeviceId.ToString() && p.DeviceNumber == NameRef && p.DeviceType == DeviceType)
            .ToListAsync();

            var data = Enumerable.Range(0, 24)
                .GroupJoin(result,
                    hour => hour,
                    item => item.ValueDate?.Hour ?? 0,
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
           .GroupBy(item => new { item.DeviceId, item.ValueDate?.Hour, item.ValueDate?.Date })
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
                   ValueDate = new DateTime(group.Key.Date.Value.Year, group.Key.Date.Value.Month, group.Key.Date.Value.Day, group.Key.Hour.Value, 0, 0),
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

        public Task<List<LogDeviceStatusEntity>> PushDataLogDeviceOnOff(DeviceDataQueryModel queryModel)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}