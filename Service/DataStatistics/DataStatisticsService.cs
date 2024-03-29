﻿using Common.Enum;
using Common.Queries;
using Dapper;
using Database;
using Entities;
using Entities.LogProcess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Models;
using Models.Config.Mongo;
using Models.Device;
using Models.DeviceData;
using Models.InstrumentSetThreshold;
using Models.LoggerProcess;
using MongoDB.Driver;
using Newtonsoft.Json.Linq;
using Service.Contracts.Logger;

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

        public DataStatisticsService(IOptions<MongoDbConfigModel> mongoDbConfig, ILoggerManager logger, DapperContext dapperContext)
        {
            this.client = new MongoClient(mongoDbConfig.Value.ConnectionString);
            var database = this.client.GetDatabase(mongoDbConfig.Value.DatabaseName);
            instrumentValue = database.GetCollection<InstrumentValueByFiveSecondEntity>(mongoDbConfig.Value.CollectionName);
            logProcess = database.GetCollection<LogProcessEntity>(mongoDbConfig.Value.CollectionLog);
            logDevice = database.GetCollection<LogDeviceStatusEntity>(mongoDbConfig.Value.CollectionLogDevice);
            this.logger = logger;
            this.dapperContext = dapperContext;
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
        public class ValueModel
        {
            public string Id { get; set; }
            public string value { get; set; }
            public string ValueDate { get; set; }
        }

        public class Value
        {
            public string ModuleId { get; set; } = null!;
            public List<ValueModel> Values { get; set; } = null!;
        }

        public async Task<List<ValueDeviceIns>> GetValueDeviceInsAsync(int zoneId)
        {
            var query = DeviceQuery.DeviceByZone;

            List<DeviceDisplayModel> devices = new List<DeviceDisplayModel>();
            using (var connection = dapperContext.CreateConnection())
            {
                connection.Open();
                var result = await connection.QueryAsync<DeviceDisplayModel>(query, new { Id = zoneId });
                connection.Close();
                devices.AddRange(result);
            }

            var moduleIds = devices.Select(p => p.ModuleId).Distinct().ToList();

            List<ValueDeviceIns> kq = new List<ValueDeviceIns> { };

            foreach (var moduleId in moduleIds)
            {
                var resultFromMg = await instrumentValue.Find(p => p.ModuleId!.Equals(moduleId.ToString(), StringComparison.OrdinalIgnoreCase)).SortByDescending(p => p.ValueDate).ToListAsync();

                //Payloads.Add(result.PayLoad ?? "");
                if (resultFromMg.Any())
                {
                    int count = 0;
                    foreach (var itemResultFromMg in resultFromMg)
                    {
                        JObject json = JObject.Parse(itemResultFromMg.PayLoad ?? "{}");

                        var deviceIds = devices.Where(p => p.ModuleId == moduleId).Distinct().ToList();

                        foreach (var deviceId in deviceIds)
                        {
                            if (json.ContainsKey(deviceId.Id.ToString()))
                            {
                                var value = (string?)json[deviceId.Id.ToString()] ?? string.Empty;
                                // Tiếp tục xử lý giá trị nếu cần
                                kq.Add(new ValueDeviceIns()
                                {
                                    ModuleId = moduleId,
                                    Id = deviceId.Id,
                                    ValueDevice = value,
                                    DateValue = itemResultFromMg.ValueDate,
                                    DateCreated = deviceId.DateCreated,
                                    Description = deviceId.Description,
                                    DeviceType = deviceId.DeviceType,
                                    Gate = deviceId.Gate,
                                    IsAction = deviceId.IsAction,
                                    IsAuto = deviceId.IsAuto,
                                    IsErrored = false,
                                    IsUsed = deviceId.IsUsed,
                                    Name = deviceId.Name,
                                    NameRef = deviceId.NameRef,
                                    Unit = deviceId.Unit,
                                    ZoneId = deviceId.ZoneId
                                });
                            }
                            else
                            {
                                // Xử lý trường hợp khi không tìm thấy khóa
                                kq.Add(new ValueDeviceIns()
                                {
                                    ModuleId = moduleId,
                                    Id = deviceId.Id,

                                    ValueDevice = "",
                                    DateValue = itemResultFromMg.ValueDate,

                                    DateCreated = deviceId.DateCreated,
                                    Description = deviceId.Description,
                                    DeviceType = deviceId.DeviceType,
                                    Gate = deviceId.Gate,
                                    IsAction = deviceId.IsAction,
                                    IsAuto = deviceId.IsAuto,
                                    IsErrored = true,

                                    IsUsed = deviceId.IsUsed,
                                    Name = deviceId.Name,
                                    NameRef = deviceId.NameRef,
                                    Unit = deviceId.Unit,
                                    ZoneId = deviceId.ZoneId
                                });
                            }
                            count++;
                        }
                        if (count >= deviceIds.Count)
                        {
                            break;
                        }
                    }
                }
                else
                {
                    var deviceIds = devices.Where(p => p.ModuleId == moduleId).Distinct().ToList();
                    foreach (var deviceId in deviceIds)
                    {
                        kq.Add(new ValueDeviceIns()
                        {
                            ModuleId = moduleId,
                            Id = deviceId.Id,

                            ValueDevice = "",
                            DateValue = null,

                            DateCreated = deviceId.DateCreated,
                            Description = deviceId.Description,
                            DeviceType = deviceId.DeviceType,
                            Gate = deviceId.Gate,
                            IsAction = deviceId.IsAction,
                            IsAuto = deviceId.IsAuto,
                            IsErrored = false,
                            IsUsed = deviceId.IsUsed,
                            Name = deviceId.Name,
                            NameRef = deviceId.NameRef,
                            Unit = deviceId.Unit,
                            ZoneId = deviceId.ZoneId
                        });
                    }
                }
            }
            return kq;
        }

        // By Date
        //public async Task<List<StatisticByDateDisplayModel>> StatisticsByDateDataDevices(StatisticQueryModel model)
        //{
        //    StatisticType type;
        //    FunctionDeviceType nameRef = FunctionDeviceType.None;
        //    string deviceType = "";

        //    using (var connection = dapperContext.CreateConnection())
        //    {
        //        connection.Open();

        //        // Replace "YourQueryHere" with your SQL query or stored procedure
        //        string query = "SELECT TypeStatis,DeviceType, NameRef  FROM [Device] WHERE Id = @DeviceId";

        //        // Replace YourModelType with the actual type of your model
        //        var result = await connection.QueryAsync<DeviceTypeGet>(query, new { DeviceId = model.DeviceId });

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
        //            return await StatisticsByDateDataDevicesValueDouble(model.DeviceId, nameRef, deviceType, model.ValueDate);
        //        case StatisticType.ValueOnOff:
        //            return await StatisticsByDateDataDevicesValueOnOff(model.DeviceId, nameRef, deviceType);
        //        case StatisticType.ValueDetect:
        //            return await StatisticsByDateDataDevicesValueDouble(model.DeviceId, nameRef, deviceType, model.ValueDate);
        //    }
        //    return await StatisticsByDateDataDevicesValueDouble(model.DeviceId, nameRef, deviceType, model.ValueDate);
        //}
        //private async Task<List<StatisticByDateDisplayModel>> StatisticsByDateDataDevicesValueDouble(Guid DeviceId, FunctionDeviceType NameRef, string DeviceType, DateTime ValueDate)
        //{
        //    string Nameref = ((FunctionDeviceType)NameRef).AsString(EnumFormat.Description)!;
        //    var result = await instrumentValue.Find(p => p.DeviceId!.Equals(DeviceId.ToString(), StringComparison.OrdinalIgnoreCase) && p.DeviceType == Nameref && p.ValueDate.Date == ValueDate.Date && p.PayLoad != "nan")
        //    .ToListAsync();
        //    List<StatisticByDateDisplayModel> data = new List<StatisticByDateDisplayModel>();

        //    var result1 = result.Where(p => p.DeviceNumber == Nameref).ToList();
        //    var data1 = Enumerable.Range(0, 24)
        //   .GroupJoin(result1,
        //       hour => hour,
        //       item => item.ValueDate.Hour,
        //       (hour, group) => new
        //       {
        //           Hour = hour,
        //           Values = group.Select(item => double.TryParse(item.PayLoad, out double payloadValue) ? payloadValue : 0).ToList()
        //       })
        //   .Select(group =>
        //   {
        //       var values = group.Values;
        //       return new StatisticByDateDisplayModel
        //       {
        //           NameDevice = DeviceId.ToString(),
        //           Type = StatisticType.ValueDouble,
        //           ValueAVG = values.Any() ? values.Average() : 0,
        //           ValueMAX = values.Any() ? values.Max() : 0,
        //           ValueMIN = values.Any() ? values.Min() : 0,
        //           CountTotal = values.Count,
        //           ValueDate = ValueDate.AddHours(group.Hour)
        //       };
        //   })
        //   .OrderBy(item => item.ValueDate)
        //   .ToList();
        //    data.AddRange(data1);

        //    // Sử dụng JsonNumberHandling.AllowNamedFloatingPointLiterals
        //    var jsonSerializerOptions = new JsonSerializerOptions
        //    {
        //        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        //        NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals,
        //        WriteIndented = true
        //    };

        //    var jsonData = JsonSerializer.Serialize(data, jsonSerializerOptions);
        //    return data;
        //}

        //private async Task<List<StatisticByDateDisplayModel>> StatisticsByDateDataDevicesValueOnOff(Guid DeviceId, FunctionDeviceType NameRef, string DeviceType)
        //{
        //    string Nameref = ((FunctionDeviceType)NameRef).AsString(EnumFormat.Description)!;
        //    var result = await instrumentValue.Find(p => p.DeviceId == DeviceId.ToString() && p.DeviceNumber == Nameref && p.DeviceType == DeviceType).ToListAsync();

        //    var data = result
        //   .GroupBy(item => new { item.DeviceId, item.ValueDate.Date })
        //   .Select(group =>
        //   {
        //       var values = group.Select(item => double.Parse(item.PayLoad ?? "_")).ToList();
        //       return new StatisticByDateDisplayModel
        //       {
        //           NameDevice = group.Key.DeviceId,
        //           Type = StatisticType.ValueDouble,  // Đặt kiểu thống kê tương ứng
        //           CountOn = group.Count(e => e.PayLoad == "1"),
        //           CountOff = group.Count(e => e.PayLoad == "0"),
        //           CountTotal = group.Count(),
        //           ValueDate = group.Key.Date
        //       };
        //   })
        //   .ToList();

        //    return data;
        //}


        //private async Task<List<StatisticByDateDisplayModel>> StatisticsByHourDataDevicesValueDouble(Guid DeviceId, string NameRef, string DeviceType)
        //{
        //    var result = await instrumentValue.Find(p => p.DeviceId == DeviceId.ToString() && p.DeviceNumber == NameRef && p.DeviceType == DeviceType)
        //    .ToListAsync();

        //    var data = Enumerable.Range(0, 24)
        //        .GroupJoin(result,
        //            hour => hour,
        //            item => item.ValueDate.Hour,
        //            (hour, group) => new
        //            {
        //                Hour = hour,
        //                Values = group.Select(item => double.Parse(item.PayLoad ?? "0")).ToList()
        //            })
        //        .Select(group =>
        //        {
        //            var values = group.Values;
        //            return new StatisticByDateDisplayModel
        //            {
        //                NameDevice = DeviceId.ToString(), // Thiết lập tên thiết bị (bạn có thể muốn cập nhật theo yêu cầu của mình)
        //                Type = StatisticType.ValueDouble,
        //                ValueAVG = values.Any() ? values.Average() : 0,
        //                ValueMAX = values.Any() ? values.Max() : 0,
        //                ValueMIN = values.Any() ? values.Min() : 0,
        //                CountTotal = values.Count,
        //                ValueDate = DateTime.Today.AddHours(group.Hour)
        //            };
        //        })
        //        .OrderBy(item => item.ValueDate)
        //        .ToList();

        //    return data;
        //}
        //private async Task<List<StatisticByDateDisplayModel>> StatisticsByHourDataDevicesValueOnOff(Guid DeviceId, string NameRef, string DeviceType)
        //{
        //    var result = await instrumentValue.Find(p => p.DeviceId == DeviceId.ToString() && p.DeviceNumber == NameRef && p.DeviceType == DeviceType).ToListAsync();

        //    var data = result
        //   .GroupBy(item => new { item.DeviceId, item.ValueDate.Hour, item.ValueDate.Date })
        //   .Select(group =>
        //   {
        //       var values = group.Select(item => double.Parse(item.PayLoad ?? "_")).ToList();
        //       return new StatisticByDateDisplayModel
        //       {
        //           NameDevice = group.Key.DeviceId,
        //           Type = StatisticType.ValueDouble,  // Đặt kiểu thống kê tương ứng
        //           CountOn = group.Count(e => e.PayLoad == "1"),
        //           CountOff = group.Count(e => e.PayLoad == "0"),
        //           CountTotal = group.Count(),
        //           ValueDate = new DateTime(group.Key.Date.Year, group.Key.Date.Month, group.Key.Date.Day, group.Key.Hour, 0, 0),
        //       };
        //   })
        //   .ToList();

        //    return data;
        //}
        #region Log Device ONOFF
        public async Task PushDataLogDeviceOnOff(List<LogDeviceStatusEntity> addModels)
        {
            await logDevice.InsertManyAsync(addModels);
        }

        public async Task<BaseResModel<LogDeviceStatusEntity>> GetDataLogDeviceOnOff(LogDeviceDataQueryModel queryModel)
        {
            try
            {
                var result = await logDevice.Find(_ => true).ToListAsync();
                if (queryModel.ValueDate != null)
                {
                    result = result.Where(prop => prop.ValueDate!.Value.AddHours(+7).Date == queryModel.ValueDate.Value.AddHours(+7).Date).ToList();
                }
                if (queryModel.TypeOnOff != null)
                {
                    result = result.Where(prop => prop.TypeOnOff == (int)queryModel.TypeOnOff).ToList();
                }
                if (queryModel.ThresholdId != null)
                {
                    result = result.Where(prop => prop.ThresholdId == queryModel.ThresholdId).ToList();
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
            catch
            {
                throw;
            }

        }
        #endregion

        public async Task<List<OnOffDeviceByThresholdModel>> GetValueDeviceForThreshold(IEnumerable<InstrumentationGetForSystem> model)
        {
            try
            {
                var result = new List<OnOffDeviceByThresholdModel>();
                foreach (var itemModel in model)
                {
                    //logger.LogInformation("Time: " + DateTime.UtcNow.AddHours(+7).Round(TimeSpan.FromMinutes(1)));

                    var Value = await instrumentValue.Find(p => p.ModuleId!.ToLower() == itemModel.ModuleSensorId.ToString().ToLower()
                    //&& DateTime.UtcNow.AddHours(+7).Round(TimeSpan.FromMinutes(1)) == p.ValueDate.Date.Round(TimeSpan.FromMinutes(1))
                    ).SortByDescending(p => p.ValueDate).FirstOrDefaultAsync();

                    if (Value != null)
                    {
                        string lablePayload = itemModel.InstrumentationId.ToString();

                        JObject jsonObject = JObject.Parse(Value.PayLoad!);

                        int value = (dynamic?)jsonObject[lablePayload] ?? -1000;

                        if (value != -1000)
                        {
                            if (itemModel.NameRefSensor != FunctionDeviceType.RainDetection)
                            {
                                if (itemModel.OnInUpperThreshold)
                                {
                                    if (itemModel.ThresholdValueOn < value && !itemModel.DeviceDriverAction)
                                    {

                                        // Logic mở thiết bị điều khiển
                                        var OnOff = new OnOffDeviceByThresholdModel()
                                        {
                                            DeviceId = itemModel.DeviceDriverId,
                                            DeviceName = itemModel.NameDeviceDriver,
                                            DeviceType = DeviceType.W,
                                            ModuleId = itemModel.ModuleDriverId,
                                            RequestOn = true,
                                            ThresholdId = itemModel.Id,
                                            ValueSensor = value.ToString()
                                        };
                                        result.Add(OnOff);
                                    }
                                    else if (itemModel.ThresholdValueOff > value && itemModel.DeviceDriverAction)
                                    {
                                        // Logic đóng thiết bị điều khiển
                                        var OnOff = new OnOffDeviceByThresholdModel()
                                        {
                                            DeviceId = itemModel.DeviceDriverId,
                                            DeviceName = itemModel.NameDeviceDriver,
                                            DeviceType = DeviceType.W,
                                            ModuleId = itemModel.ModuleDriverId,
                                            RequestOn = false,
                                            ThresholdId = itemModel.Id,
                                            ValueSensor = value.ToString()
                                        };
                                        result.Add(OnOff);
                                    }
                                }
                                else
                                {
                                    if (itemModel.ThresholdValueOn < value && itemModel.DeviceDriverAction)
                                    {
                                        // Logic đóng thiết bị điều khiển 
                                        var OnOff = new OnOffDeviceByThresholdModel()
                                        {
                                            DeviceId = itemModel.DeviceDriverId,
                                            DeviceName = itemModel.NameDeviceDriver,
                                            DeviceType = DeviceType.W,
                                            ModuleId = itemModel.ModuleDriverId,
                                            RequestOn = false,
                                            ThresholdId = itemModel.Id,
                                            ValueSensor = value.ToString()
                                        };
                                        result.Add(OnOff);
                                    }
                                    else if (itemModel.ThresholdValueOff > value && !itemModel.DeviceDriverAction)
                                    {
                                        // Logic mở thiết bị điều khiển
                                        var OnOff = new OnOffDeviceByThresholdModel()
                                        {
                                            DeviceId = itemModel.DeviceDriverId,
                                            DeviceName = itemModel.NameDeviceDriver,
                                            DeviceType = DeviceType.W,
                                            ModuleId = itemModel.ModuleDriverId,
                                            RequestOn = true,
                                            ThresholdId = itemModel.Id,
                                            ValueSensor = value.ToString()
                                        };
                                        result.Add(OnOff);
                                    }
                                }
                            }
                            else
                            {
                                if (itemModel.OnInUpperThreshold)
                                {
                                    if (!itemModel.DeviceDriverAction && itemModel.ThresholdValueOn == 1)
                                    {
                                        // Logic mở thiết bị điều khiển
                                        var OnOff = new OnOffDeviceByThresholdModel()
                                        {
                                            DeviceId = itemModel.DeviceDriverId,
                                            DeviceName = itemModel.NameDeviceDriver,
                                            DeviceType = DeviceType.W,
                                            ModuleId = itemModel.ModuleDriverId,
                                            RequestOn = true,
                                            ThresholdId = itemModel.Id,
                                            ValueSensor = value.ToString()
                                        };
                                        result.Add(OnOff);
                                    }
                                    else if (itemModel.DeviceDriverAction && itemModel.ThresholdValueOn == 0)
                                    {
                                        // Logic đóng thiết bị điều khiển 
                                        var OnOff = new OnOffDeviceByThresholdModel()
                                        {
                                            DeviceId = itemModel.DeviceDriverId,
                                            DeviceName = itemModel.NameDeviceDriver,
                                            DeviceType = DeviceType.W,
                                            ModuleId = itemModel.ModuleDriverId,
                                            RequestOn = false,
                                            ThresholdId = itemModel.Id,
                                            ValueSensor = value.ToString()
                                        };
                                        result.Add(OnOff);
                                    }
                                }
                                else
                                {
                                    if (itemModel.DeviceDriverAction && itemModel.ThresholdValueOn == 1)
                                    {
                                        // Logic đóng thiết bị điều khiển 
                                        var OnOff = new OnOffDeviceByThresholdModel()
                                        {
                                            DeviceId = itemModel.DeviceDriverId,
                                            DeviceName = itemModel.NameDeviceDriver,
                                            DeviceType = DeviceType.W,
                                            ModuleId = itemModel.ModuleDriverId,
                                            RequestOn = false,
                                            ThresholdId = itemModel.Id,
                                            ValueSensor = value.ToString()
                                        };
                                        result.Add(OnOff);
                                    }
                                    else if (!itemModel.DeviceDriverAction && itemModel.ThresholdValueOn == 1)
                                    {
                                        // Logic mở thiết bị điều khiển
                                        var OnOff = new OnOffDeviceByThresholdModel()
                                        {
                                            DeviceId = itemModel.DeviceDriverId,
                                            DeviceName = itemModel.NameDeviceDriver,
                                            DeviceType = DeviceType.W,
                                            ModuleId = itemModel.ModuleDriverId,
                                            RequestOn = true,
                                            ThresholdId = itemModel.Id,
                                            ValueSensor = value.ToString()
                                        };
                                        result.Add(OnOff);
                                    }
                                }
                            }
                        }
                    }
                }
                return result;
            }
            catch
            {
                throw;
            }
        }
    }
}