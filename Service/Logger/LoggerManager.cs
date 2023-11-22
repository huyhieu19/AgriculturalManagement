using Common.Enum;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Models;
using Models.Config.Mongo;
using MongoDB.Driver;
using NLog;
using Service.Contracts.Logger;

namespace Service.Logger
{
    public sealed class LoggerManager : ILoggerManager
    {
        private static ILogger logger = LogManager.GetCurrentClassLogger();
        private readonly IMongoCollection<LogProcessEntity> logProcess;
        private readonly MongoClient client;
        private readonly IHttpContextAccessor _contextAccessor;

        public LoggerManager(IOptions<MongoDbConfigModel> mongoDbConfig, IHttpContextAccessor _contextAccessor)
        {
            this.client = new MongoClient(mongoDbConfig.Value.ConnectionString);
            var database = this.client.GetDatabase(mongoDbConfig.Value.DatabaseName);
            logProcess = database.GetCollection<LogProcessEntity>(mongoDbConfig.Value.CollectionLog);
            this._contextAccessor = _contextAccessor;
        }

        public void LogDebug(string message, LogProcessModel? logProcessModel = null)
        {
            logger.Debug(message);
            if (logProcessModel != null)
            {
                Task.Run(async () =>
                {
                    var UserName = logProcessModel.User ?? _contextAccessor.HttpContext!.User.FindFirst("Email")!.Value;
                    await logProcess.InsertOneAsync(new LogProcessEntity()
                    {
                        LoggerProcessType = logProcessModel.LoggerProcessType.ToString(),
                        LogMessage = message,
                        ServiceName = logProcessModel.ServiceName,
                        LogMessageDetail = logProcessModel.LogMessageDetail,
                        ValueDate = DateTime.UtcNow,
                        LoggerType = LoggerType.Debug.ToString(),
                        User = UserName,
                    });
                });
            }
        }

        public void LogError(string message, LogProcessModel? logProcessModel = null)
        {
            logger.Error(message);
            if (logProcessModel != null)
            {
                Task.Run(async () =>
                {
                    var UserName = logProcessModel.User ?? _contextAccessor.HttpContext!.User.FindFirst("Email")!.Value;
                    await logProcess.InsertOneAsync(new LogProcessEntity()
                    {
                        LoggerProcessType = logProcessModel.LoggerProcessType.ToString(),
                        LogMessage = message,
                        ServiceName = logProcessModel.ServiceName,
                        LogMessageDetail = logProcessModel.LogMessageDetail,
                        ValueDate = DateTime.UtcNow,
                        LoggerType = LoggerType.Error.ToString(),
                        User = UserName,
                    });
                });
            }
        }

        public void LogInformation(string message, LogProcessModel? logProcessModel = null)
        {
            logger.Info(message);
            if (logProcessModel != null)
            {
                Task.Run(async () =>
                {
                    var UserName = logProcessModel.User ?? _contextAccessor.HttpContext!.User.FindFirst("Email")!.Value;
                    await logProcess.InsertOneAsync(new LogProcessEntity()
                    {
                        LoggerProcessType = logProcessModel.LoggerProcessType.ToString(),
                        LogMessage = message,
                        ServiceName = logProcessModel.ServiceName,
                        LogMessageDetail = logProcessModel.LogMessageDetail,
                        ValueDate = DateTime.UtcNow,
                        LoggerType = LoggerType.Informmation.ToString(),
                        User = UserName,
                    });
                });
            }
        }

        public void LogWarning(string message, LogProcessModel? logProcessModel = null)
        {
            logger.Warn(message);
            if (logProcessModel != null)
            {
                Task.Run(async () =>
                {
                    var UserName = logProcessModel.User ?? _contextAccessor.HttpContext!.User.FindFirst("Email")!.Value;
                    await logProcess.InsertOneAsync(new LogProcessEntity()
                    {
                        LoggerProcessType = logProcessModel.LoggerProcessType.ToString(),
                        LogMessage = message,
                        ServiceName = logProcessModel.ServiceName,
                        LogMessageDetail = logProcessModel.LogMessageDetail,
                        ValueDate = DateTime.UtcNow,
                        LoggerType = LoggerType.Warning.ToString(),
                        User = UserName,
                    });
                });
            }
        }
    }
}