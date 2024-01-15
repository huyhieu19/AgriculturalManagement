// Ignore Spelling: Mqtt

using Common.Enum;
using Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Models;
using Models.Config.Mqtt;
using Newtonsoft.Json.Linq;
using Service;
using Service.Contracts.Logger;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace MQTTProcess
{

    public class ProcessJobMqtt : BackgroundService
    {
        private readonly ILoggerManager logger;
        private readonly IConfiguration configuration;
        private static List<InstrumentValueByFiveSecondEntity> _messageList = new List<InstrumentValueByFiveSecondEntity>();
        private static int _messageCount = 0;
        private const int MaxMessageCount = 5;
        private readonly IDataStatisticsService dataStatisticsService;
        private static MqttClient? mqttClient = null;

        public ProcessJobMqtt(ILoggerManager logger, IConfiguration configuration,
            IDataStatisticsService dataStatisticsService
            )
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
                        string mqttTopic = $"{connection.SystemId}/r/#";

                        mqttClient.MqttMsgPublishReceived += client_MqttMsgPublishReceived;

                        mqttClient.Subscribe(new[] { mqttTopic }, new[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                        logger.LogInformation("Sub -> " + mqttTopic);
                    }
                    await Task.Delay(TimeSpan.FromSeconds(10));
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

            Task.Run(async () => await ProcessJobReceivedPayload(payload, e.Topic));
        }

        private async Task ProcessJobReceivedPayload(string payload, string Topic)
        {
            string[] topicSplits = Topic.Split("/");
            //Create an InstrumentValueByFiveSecondEntity object for the incoming data
            string deviceId = topicSplits[2];


            if (topicSplits[1] == "r" && topicSplits[3] == "ND_DA")
            {
                JObject jsonData = JObject.Parse(payload);
                // Truy cập các trường trong đối tượng JSON
                string temperature = (string)jsonData["ND"]!; // 1
                string humidity = (string)jsonData["DA"]!; // 2
                logger.LogInformation($"Receiver Humidity: {humidity}");

                //await deviceJobInstrumentation.RunningJobThreshold(new Guid(deviceId), temperature);


                //var entity1 = new InstrumentValueByFiveSecondEntity()
                //{
                //    PayLoad = temperature,
                //    DeviceId = deviceId,
                //    ValueDate = DateTime.UtcNow.AddHours(+7),
                //    DeviceNumber = "ND",
                //};

                //await ProcessAndSaveDataAsync(entity1);
                var entity2 = new InstrumentValueByFiveSecondEntity()
                {
                    PayLoad = humidity,
                    DeviceId = deviceId,
                    ValueDate = DateTime.UtcNow.AddHours(+7),
                    DeviceNumber = "DA",
                };
                await ProcessAndSaveDataAsync(entity2);

            }
            if (topicSplits[1] == "w")
            {
                // code đóng mở trạng thái thiết bị ở đây
            }
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
    }
}
