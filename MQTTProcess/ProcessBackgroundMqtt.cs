// Ignore Spelling: Mqtt

using Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Service.Contracts.Logger;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace MQTTProcess
{
    public class ProcessBackgroundMqtt : BackgroundService
    {

        private MqttClient? mqttClient;
        private readonly ILoggerManager logger;
        private readonly IConfiguration _configuration;

        public ProcessBackgroundMqtt(ILoggerManager logger, IConfiguration _configuration)
        {
            this._configuration = _configuration;
            this.logger = logger;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            string SystemId = _configuration.GetSection("SystemId").Get<string>()!;
            while (!stoppingToken.IsCancellationRequested)
            {
                logger.LogInformation("Check Connection to MQTT Broker");
                if (mqttClient == null || !mqttClient.IsConnected)
                {
                    mqttClient = Mqtt.ConnectMQTT("broker.emqx.io", 1883, "abc", "abc", "abc");
                    List<string> mqttTopics = new();
                    List<byte> msgBases = new();
                    logger.LogInformation("Connected to MQTT Broker");

                    string mqttTopic = $"{SystemId}/#";
                    mqttTopics.Add(mqttTopic);
                    msgBases.Add(MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE);
                    logger.LogInformation("Sub -> " + mqttTopic);

                    Subscribe(mqttClient!, mqttTopics.ToArray(), msgBases.ToArray());
                }

                await Task.Delay(TimeSpan.FromSeconds(5));
            }
        }
        private void Subscribe(MqttClient client1, string[] topics, byte[] msgBases)
        {
            client1.MqttMsgPublishReceived += client_MqttMsgPublishReceived;
            client1.Subscribe(topics, msgBases);
        }

        // Receive data and call data processing functions
        private void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            string payload = System.Text.Encoding.Default.GetString(e.Message);

            logger.LogInformation($"Received `{payload}` from `{e.Topic}` topic");

            // Create an InstrumentValueByFiveSecondEntity object for the incoming data
            var entity = new InstrumentValueByFiveSecondEntity()
            {
                PayLoad = payload,
                Topic = e.Topic,
                ValueDate = DateTime.Now
            };
        }
    }
}
