using Common.Enum;
using Microsoft.Extensions.Configuration;
using Models;
using Models.Config.Mqtt;
using Models.DeviceControl;
using Service.Contracts.Logger;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace MQTTProcess
{
    public interface IProcessJobControlDevice
    {
        Task<bool> OnOffDevice(OnOffDeviceQueryModel model);
        Task<bool> AsyncStatusDeviceControl(List<StatusDeviceControlModel> models);
    }
    public class ProcessJobControlDevice : IProcessJobControlDevice
    {
        private readonly ILoggerManager logger;
        private readonly IConfiguration configuration;
        private static MqttClient? mqttClient = null;

        public ProcessJobControlDevice(ILoggerManager logger, IConfiguration configuration)
        {
            this.logger = logger;
            this.configuration = configuration;
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

                string topicSub = $"{connection.SystemId}/w/#";
                string topicPub = $"{model.ModuleId.ToString().ToUpper()}/w/{model.DeviceId.ToString().ToUpper()}";
                mqttClient = ConnectMQTT(connection.ServerName, connection.Port, connection.ClientId, connection.UserName, connection.UserPW);

                // Define a TaskCompletionSource to signal completion
                var tcs = new TaskCompletionSource<bool>();
                void Subscribe(MqttClient client, string subscribeTopic)
                {
                    //Set up event handler
                    client.MqttMsgPublishReceived += (sender, e) =>
                    {
                        string[] topicSplits = e.Topic.Split('/');
                        string payload = System.Text.Encoding.Default.GetString(e.Message);
                        if (topicSplits[2].ToLower() == model.DeviceId.ToString().ToLower() && topicSplits[1].ToLower() == "w" && payload.ToLower() == "c")
                        {
                            // Signal completion
                            logger.LogInformation("Receive turn on device from mqtt");
                            tcs.TrySetResult(true);
                        };
                    };
                    client.Subscribe(new string[] { subscribeTopic }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                }
                //Subscribe to topic
                Subscribe(mqttClient, topicSub);
                logger.LogInformation($"Subscribing topic {topicSub}");
                // Publish the message

                if (model.RequestOn)
                {
                    // Chạy 5 lần nếu không được thì return false
                    if (mqttClient == null || !mqttClient.IsConnected)
                    {
                        mqttClient = ConnectMQTT(connection.ServerName, connection.Port, connection.ClientId, connection.UserName, connection.UserPW);
                    }
                    logger.LogInformation($"Mo thiet bi {topicPub}");
                    mqttClient.Publish(topicPub, System.Text.Encoding.UTF8.GetBytes("1"));
                }
                else if (!model.RequestOn)
                {
                    if (mqttClient == null || !mqttClient.IsConnected)
                    {
                        mqttClient = ConnectMQTT(connection.ServerName, connection.Port, connection.ClientId, connection.UserName, connection.UserPW);
                    }
                    mqttClient.Publish(topicPub, System.Text.Encoding.UTF8.GetBytes("0"));
                    logger.LogInformation($"dong thiet bi {topicPub}");
                }

                logger.LogInformation("finished publish");


                // Wait for completion or timeout (adjust timeout value as needed)
                var timeoutTask = Task.Delay(TimeSpan.FromSeconds(10)); // Adjust timeout as needed
                var completedTask = await Task.WhenAny(tcs.Task, timeoutTask);

                if (completedTask == tcs.Task)
                {
                    // The task completed successfully
                    mqttClient.Unsubscribe(new[] { topicSub });
                    return tcs.Task.Result;
                }
                else
                {
                    // Timeout occurred
                    mqttClient.Unsubscribe(new[] { topicSub });
                    return false;
                }
                //if (complete)
                //{
                //    return await Task.FromResult(true);
                //}
                //return await Task.FromResult(false);

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
        // Đồng bộ trạng thái thiết bị mới nhất
        public async Task<bool> AsyncStatusDeviceControl(List<StatusDeviceControlModel> models)
        {
            try
            {
                var connection = GetConnection();
                for (int i = 0; i < models.Count(); i++)
                {
                    string topic = $"{models[i].ModuleId.ToString().ToUpper()}/w/{models[i].Id.ToString().ToUpper()}";
                    if (models[i].IsAction == true)
                    {
                        if (mqttClient == null || !mqttClient.IsConnected)
                        {
                            mqttClient = ConnectMQTT(connection.ServerName, connection.Port, connection.ClientId, connection.UserName, connection.UserPW);
                        }
                        mqttClient.Publish(topic, System.Text.Encoding.UTF8.GetBytes("1"));
                    }
                    else if (models[i].IsAction == false)
                    {
                        if (mqttClient == null || !mqttClient.IsConnected)
                        {
                            mqttClient = ConnectMQTT(connection.ServerName, connection.Port, connection.ClientId, connection.UserName, connection.UserPW);
                        }
                        mqttClient.Publish(topic, System.Text.Encoding.UTF8.GetBytes("0"));
                    }
                }
                return await Task.FromResult(true);
            }
            catch
            {
                logger.LogInformation("Error async status");
                throw;
            }
        }
        private MqttClient ConnectMQTT(string broker, int port, string clientId, string username, string password)
        {
            try
            {
                MqttClient client = new MqttClient(broker, port, false, MqttSslProtocols.None, null, null);
                client.Connect(clientId, username, password);
                return client;
            }
            catch
            {
                throw;
            }
        }
    }
}
