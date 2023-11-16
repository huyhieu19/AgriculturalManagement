// Ignore Spelling: Mqtt

using Common.TimeHelper;
using Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Models.Config.Mongo;
using Models.Config.Mqtt;
using MongoDB.Bson.Serialization.IdGenerators;
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



        public ProcessJobMqtt(ILoggerManager logger, IConfiguration configuration,
            IDataStatisticsService dataStatisticsService)
        {
            this.logger = logger;
            this.dataStatisticsService = dataStatisticsService;
            this.configuration = configuration;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            MqttClient? mqttClient = null;
            var connection = GetConnection();

            while (!stoppingToken.IsCancellationRequested)
            {
                logger.LogInformation("Check Connection to MQTT Broker");
                if (mqttClient == null || !mqttClient.IsConnected)
                {
                    mqttClient = Mqtt.ConnectMQTT(connection.ServerName, connection.Port, connection.ClientId, connection.UserName, connection.UserPW);
                    logger.LogInformation("Connected to MQTT Broker");
                    List<string> mqttTopics = new();
                    List<byte> msgBases = new();
                    string mqttTopic = $"{connection.SystemId}/#";

                    mqttTopics.Add(mqttTopic);
                    msgBases.Add(MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE);
                    logger.LogInformation("Sub -> " + mqttTopic);

                    mqttClient.MqttMsgPublishReceived += client_MqttMsgPublishReceived;
                    mqttClient.Subscribe(new[] { mqttTopic },new[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });

                }
                await Task.Delay(TimeSpan.FromSeconds(10));
            }
        }

        // Receive data and call data processing functions
        private void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            string payload = System.Text.Encoding.Default.GetString(e.Message);

            logger.LogInformation($"Received `{payload}` from `{e.Topic}` topic");

            string[] topicSplits = e.Topic.Split("/");
            // Create an InstrumentValueByFiveSecondEntity object for the incoming data
            var entity = new InstrumentValueByFiveSecondEntity()
            {
                PayLoad = payload,
                DeviceId = topicSplits[1],
                DeviceNameType = topicSplits[3],
                DeviceType = topicSplits[2],
                ValueDate = SetTimeZone.GetDateTimeVN()
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
    }
}
