using Common.Enum;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Models.Config.Mongo;
using MongoDB.Driver;
using NLog;
using Service.Contracts.Logger;

namespace Service.Logger
{
    public sealed class LoggerManager : ILoggerManager
    {
        private static ILogger logger = LogManager.GetCurrentClassLogger();
        private readonly IMongoCollection<LogProcessModel> logProcess;
        private readonly MongoClient client;
        private readonly IHttpContextAccessor _contextAccessor;

        public LoggerManager(IOptions<MongoDbConfigModel> mongoDbConfig, IHttpContextAccessor _contextAccessor)
        {
            this.client = new MongoClient(mongoDbConfig.Value.ConnectionString);
            var database = this.client.GetDatabase(mongoDbConfig.Value.DatabaseName);
            logProcess = database.GetCollection<LogProcessModel>(mongoDbConfig.Value.CollectionLog);
            this._contextAccessor = _contextAccessor;
        }

        public void LogDebug(string message, LogProcessModel? logProcessModel = null)
        {
            logger.Debug(message);
            if (logProcessModel != null)
            {
                Task.Run(async () =>
                {
                    var UserName = logProcessModel.User ?? _contextAccessor.HttpContext!.User.FindFirst("UserName")!.Value;
                    await logProcess.InsertOneAsync(new LogProcessModel()
                    {
                        LoggerProcessType = logProcessModel.LoggerProcessType!.ToString(),
                        LogMessage = message,
                        ServiceName = logProcessModel.ServiceName,
                        LogMessageDetail = logProcessModel.LogMessageDetail,
                        ValueDate = DateTime.Now.AddHours(+7),
                        LoggerType = LoggerType.Error.ToString(),
                        User = UserName,
                    });
                });
            }
        }

        public void LogError(string message, string? ServiceName = null, LoggerProcessType? ProcessType = LoggerProcessType.None, string? MessageDetail = null, string? User = null)
        {
            logger.Error(message);
            if (ServiceName != null)
            {
                Task.Run(async () =>
                {
                    var UserName = User ?? _contextAccessor.HttpContext!.User.FindFirst("UserName")!.Value;
                    await logProcess.InsertOneAsync(new LogProcessModel()
                    {
                        LoggerProcessType = ProcessType!.Value.ToString(),
                        LogMessage = message,
                        ServiceName = ServiceName,
                        LogMessageDetail = MessageDetail,
                        ValueDate = DateTime.Now.AddHours(+7),
                        LoggerType = LoggerType.Error.ToString(),
                        User = UserName,
                    });
                });
            }
        }

        public void LogInformation(string message, string? ServiceName = null, LoggerProcessType? ProcessType = LoggerProcessType.None, string? MessageDetail = null, string? User = null)
        {
            logger.Info(message);
            if (ServiceName != null)
            {
                Task.Run(async () =>
                {
                    var UserName = User ?? _contextAccessor.HttpContext!.User.FindFirst("UserName")!.Value;
                    await logProcess.InsertOneAsync(new LogProcessModel()
                    {
                        LoggerProcessType = ProcessType!.Value.ToString(),
                        LogMessage = message,
                        ServiceName = ServiceName,
                        LogMessageDetail = MessageDetail,
                        ValueDate = DateTime.Now.AddHours(+7),
                        LoggerType = LoggerType.Informmation.ToString(),
                        User = UserName,
                    });
                });
            }
        }

        public void LogWarning(string message, string? ServiceName = null, LoggerProcessType? ProcessType = LoggerProcessType.None, string? MessageDetail = null, string? User = null)
        {
            logger.Warn(message);
        }
    }
}