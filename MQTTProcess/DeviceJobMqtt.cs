using Microsoft.Extensions.Configuration;
using Models.Config.Mqtt;
using Service.Contracts.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace MQTTProcess
{
    public interface IDeviceJobMqtt
    {
        Task<bool> TurnOnDevice(Guid deviceId);
        Task<bool> TurnOffDevice(Guid deviceId, string DeviceType, string DeviceNameNumber);
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

        public async Task<bool> TurnOffDevice(Guid deviceId, string DeviceType, string DeviceNameNumber)
        {
            MqttClient? mqttClient = null;
            var connection = GetConnection();
            string topic = $"{connection.SystemId}/{deviceId}/{DeviceType}/{DeviceNameNumber}";
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
                        logger.LogInformation("Complete");
                    }
                };
                client.Subscribe(new string[] { subscribeTopic }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
            }

            // Subscribe to topic
            Subscribe(mqttClient, topic);

            // Publish the message
            mqttClient.Publish(topic, System.Text.Encoding.UTF8.GetBytes("N"));
            logger.LogInformation("publish");

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
        //static void Publish(MqttClient client, string topic)
        //{
        //    int msg_count = 0;
        //    while (true)
        //    {
        //        System.Threading.Thread.Sleep(1 * 1000);
        //        string? a = Console.ReadLine();
        //        string msg = a;
        //        client.Publish(topic, System.Text.Encoding.UTF8.GetBytes(msg));
        //        Console.WriteLine("Send `{0}` a to topic `{1}`", msg, topic);
        //        msg_count++;
        //    }
        //}

        //static void Subscribe(MqttClient client, string topic)
        //{
        //    client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;
        //    client.Subscribe(new string[] { topic }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
        //}
        //static void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        //{
        //    string payload = System.Text.Encoding.Default.GetString(e.Message);
        //    Console.WriteLine("Received `{0}` from `{1}` topic", payload, e.Topic.ToString());
        //}

        public Task<bool> TurnOnDevice(Guid deviceId)
        {
            throw new NotImplementedException();
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
