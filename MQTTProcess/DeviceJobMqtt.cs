using Microsoft.Extensions.Configuration;
using Models.Config.Mqtt;
using Models.DeviceControl;
using Service.Contracts.Logger;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace MQTTProcess
{
    public interface IDeviceJobMqtt
    {
        Task<bool> OnOffDevice(OnOffDeviceQueryModel model);
    }
    public class DeviceJobMqtt : IDeviceJobMqtt
    {
        private readonly IConfiguration configuration;
        private readonly ILoggerManager logger;

        public DeviceJobMqtt(IConfiguration configuration, ILoggerManager logger)
        {
            this.configuration = configuration;
            this.logger = logger;
        }

        public async Task<bool> OnOffDevice(OnOffDeviceQueryModel model)
        {
            MqttClient? mqttClient = null;
            var connection = GetConnection();
            string topic = $"{connection.SystemId}/{model.DeviceId}/{model.DeviceType}/{model.DeviceNameNumber}";
            mqttClient = Mqtt.ConnectMQTT(connection.ServerName, connection.Port, connection.ClientId, connection.UserName, connection.UserPW);

            // Define a TaskCompletionSource to signal completion
            var tcs = new TaskCompletionSource<bool>();

            // Set up event handler
            void Subscribe(MqttClient client, string subscribeTopic)
            {
                client.MqttMsgPublishReceived += (sender, e) =>
                {
                    string payload = System.Text.Encoding.Default.GetString(e.Message);
                    if (payload.ToLower() == "c")
                    {
                        // Signal completion
                        tcs.TrySetResult(true);
                        logger.LogInformation("Receive turn on by mqtt");
                    }
                };
                client.Subscribe(new string[] { subscribeTopic }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
            }

            // Subscribe to topic
            Subscribe(mqttClient, topic);

            // Publish the message
            if (model.RequestOn)
            {
                mqttClient.Publish(topic, System.Text.Encoding.UTF8.GetBytes("On"));
            }
            else if (model.RequestOn)
            {
                mqttClient.Publish(topic, System.Text.Encoding.UTF8.GetBytes("Off"));
            }

            logger.LogInformation("finished publish");

            // Wait for completion or timeout (adjust timeout value as needed)
            var timeoutTask = Task.Delay(TimeSpan.FromSeconds(10)); // Adjust timeout as needed
            var completedTask = await Task.WhenAny(tcs.Task, timeoutTask);

            if (completedTask == tcs.Task)
            {
                // The task completed successfully
                return tcs.Task.Result;
            }
            else
            {
                // Timeout occurred
                return false;
            }
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
