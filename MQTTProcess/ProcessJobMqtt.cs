// Ignore Spelling: Mqtt

using Common.Enum;
using Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Models;
using Models.Config.Mqtt;
using Models.DeviceControl;
using Service;
using Service.Contracts.Logger;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace MQTTProcess
{
    public interface IDeviceJobMqtt
    {
        Task<bool> OnOffDevice(OnOffDeviceQueryModel model);
        Task<bool> AsyncStatusDeviceControl(List<StatusDeviceControlModel> models);
    }
    public class ProcessJobMqtt : BackgroundService, IDeviceJobMqtt
    {
        private readonly ILoggerManager logger;
        private readonly IConfiguration configuration;
        private static List<InstrumentValueByFiveSecondEntity> _messageList = new List<InstrumentValueByFiveSecondEntity>();
        private static int _messageCount = 0;
        private const int MaxMessageCount = 50;
        private readonly IDataStatisticsService dataStatisticsService;
        private static MqttClient? mqttClient = null;

        public ProcessJobMqtt(ILoggerManager logger, IConfiguration configuration,
            IDataStatisticsService dataStatisticsService)
        {
            this.logger = logger;
            this.dataStatisticsService = dataStatisticsService;
            this.configuration = configuration;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                var connection = GetConnection();

                while (!stoppingToken.IsCancellationRequested)
                {
                    logger.LogInformation("Check Connection to MQTT Broker");
                    if (mqttClient == null || !mqttClient.IsConnected)
                    {
                        mqttClient = Mqtt.ConnectMQTT(connection.ServerName, connection.Port, connection.ClientId, connection.UserName, connection.UserPW);

                        logger.LogInformation("Connected to MQTT Broker");
                        string mqttTopic = $"{connection.SystemId}/#";

                        mqttClient.MqttMsgPublishReceived += client_MqttMsgPublishReceived;

                        mqttClient.Subscribe(new[] { mqttTopic }, new[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                        logger.LogInformation("Sub -> " + mqttTopic);
                    }
                    await Task.Delay(TimeSpan.FromMinutes(5));
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, new LogProcessModel()
                {
                    LoggerProcessType = LoggerProcessType.DeviceStatistic,
                    LogMessageDetail = ex.ToString(),
                    ServiceName = $"{nameof(ProcessJobMqtt)} -> {nameof(ExecuteAsync)}",
                    User = "Auto"
                });
                throw;
            }
        }

        // Receive data and call data processing functions
        private void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            string payload = System.Text.Encoding.Default.GetString(e.Message);

            logger.LogInformation($"Received `{payload}` from `{e.Topic}` topic");

            string[] topicSplits = e.Topic.Split("/");
            //Create an InstrumentValueByFiveSecondEntity object for the incoming data

            var entity = new InstrumentValueByFiveSecondEntity()
            {
                PayLoad = payload,
                DeviceId = topicSplits[2] ?? null,
                DeviceNameType = topicSplits[4] ?? null,
                DeviceType = topicSplits[3] ?? null,
                ValueDate = DateTime.UtcNow.AddHours(+7)
            };
            Task.Run(async () => await ProcessAndSaveDataAsync(entity));

        }
        // Receive data and push data to Mongodb
        private async Task ProcessAndSaveDataAsync(InstrumentValueByFiveSecondEntity entity)
        {
            // Add the entity to the list
            _messageList.Add(entity);

            // Increment the message count
            SetMessageCountPlusOne();

            if (_messageCount >= MaxMessageCount)
            {
                // If the message count reaches the maximum, process and save the data
                await dataStatisticsService.PushMultipleDataToDB(_messageList);
                // Clear the list and reset the message count
                _messageList.Clear();
                SetMessageCountToZero();
            }
            await Task.CompletedTask;
        }
        private static void SetMessageCountPlusOne()
        {
            ProcessJobMqtt._messageCount++;
        }
        private static void SetMessageCountToZero()
        {
            ProcessJobMqtt._messageCount = 0;
        }
        private MqttConnectionConfigModel GetConnection()
        {
            var systemId = configuration["MqttConfig:SystemId"];
            var mqttServer = configuration["MqttConfig:MqttServer"];
            var mqttPort = configuration.GetValue<int>("MqttConfig:MqttPort");
            var mqttClientId = configuration["MqttConfig:MqttClientId"];
            var mqttUser = configuration["MqttConfig:MqttUser"];
            var mqttPW = configuration["MqttConfig:MqttPW"];
            var connection = new MqttConnectionConfigModel()
            {
                ClientId = mqttClientId!,
                Port = mqttPort,
                ServerName = mqttServer!,
                SystemId = systemId!,
                UserName = mqttUser!,
                UserPW = mqttPW!
            };
            return connection;
        }

        public async Task<bool> OnOffDevice(OnOffDeviceQueryModel model)
        {
            try
            {
                var connection = GetConnection();
                if (mqttClient == null || !mqttClient.IsConnected)
                {
                    mqttClient = Mqtt.ConnectMQTT(connection.ServerName, connection.Port, connection.ClientId, connection.UserName, connection.UserPW);
                }
                string topic = $"{connection.SystemId}/{model.ModuleId.ToString().ToUpper()}/{model.DeviceId.ToString().ToUpper()}/control";

                // Define a TaskCompletionSource to signal completion
                var tcs = new TaskCompletionSource<bool>();

                void Subscribe(MqttClient client, string subscribeTopic)
                {
                    //Set up event handler

                    client.MqttMsgPublishReceived += (sender, e) =>
                    {
                        string[] topicSplits = e.Topic.Split('/');
                        string payload = System.Text.Encoding.Default.GetString(e.Message);
                        if (topicSplits.Length == 5 && topicSplits[2] == model.DeviceId.ToString() && payload.ToLower() == "c")
                        {
                            // Signal completion
                            logger.LogInformation("Receive turn on device from mqtt");
                            tcs.TrySetResult(true);
                        };
                    };
                    client.Subscribe(new string[] { subscribeTopic }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                }

                // Subscribe to topic
                Subscribe(mqttClient, topic);

                // Publish the message
                if (model.RequestOn)
                {
                    mqttClient.Publish(topic, System.Text.Encoding.UTF8.GetBytes("1"));
                }
                else if (!model.RequestOn)
                {
                    mqttClient.Publish(topic, System.Text.Encoding.UTF8.GetBytes("0"));
                }

                logger.LogInformation("finished publish");

                // Wait for completion or timeout (adjust timeout value as needed)
                var timeoutTask = Task.Delay(TimeSpan.FromSeconds(10)); // Adjust timeout as needed
                var completedTask = await Task.WhenAny(tcs.Task, timeoutTask);

                if (completedTask == tcs.Task)
                {
                    // The task completed successfully
                    mqttClient.Unsubscribe(new[] { topic });
                    return tcs.Task.Result;
                }
                else
                {
                    // Timeout occurred
                    mqttClient.Unsubscribe(new[] { topic });
                    return false;
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, new LogProcessModel()
                {
                    LoggerProcessType = LoggerProcessType.DeviceControl,
                    LogMessageDetail = ex.ToString(),
                    ServiceName = $"{nameof(ProcessJobMqtt)} -> {nameof(OnOffDevice)}",
                });
                throw;
            }
        }

        public async Task<bool> AsyncStatusDeviceControl(List<StatusDeviceControlModel> models)
        {
            var connection = GetConnection();
            if (mqttClient == null || !mqttClient.IsConnected)
            {
                mqttClient = Mqtt.ConnectMQTT(connection.ServerName, connection.Port, connection.ClientId, connection.UserName, connection.UserPW);
            }
            for (int i = 0; i < models.Count(); i++)
            {
                string topic = $"{connection.SystemId}/{models[i].ModuleId.ToString().ToUpper()}/{models[i].Id.ToString().ToUpper()}/control";
                if (models[i].IsAction == true)
                {
                    mqttClient.Publish(topic, System.Text.Encoding.UTF8.GetBytes("1"));
                }
                else if (models[i].IsAction == false)
                {
                    mqttClient.Publish(topic, System.Text.Encoding.UTF8.GetBytes("0"));
                }
            }
            return await Task.FromResult(true);
        }
    }
}
