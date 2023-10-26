using Entities;
using Microsoft.Extensions.Hosting;
using Service;
using Service.Contracts;
using Service.Contracts.ESP;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace MQTTProcess
{
    public interface ICustomServiceStopper
    {
        Task RestartJobBackground();
    }
    public class UploadToMongoDb : BackgroundService, ICustomServiceStopper
    {
        private readonly ILoggerManager logger;
        private readonly IDataStatisticsService dataStatisticsService;
        private readonly IEspBackgroundProcessService espBackgroundProcessService;
        private static List<InstrumentValueByFiveSecondEntity> messageList = new List<InstrumentValueByFiveSecondEntity>();
        private static int messageCount = 0;
        private const int maxMessageCount = 50;
        private static bool isBreak = false;
        private MqttClient? client;


        public UploadToMongoDb(IDataStatisticsService dataStatisticsService,
            IEspBackgroundProcessService espBackgroundProcessService,
            ILoggerManager logger)
        {
            this.dataStatisticsService = dataStatisticsService;
            this.espBackgroundProcessService = espBackgroundProcessService;
            this.logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                logger.LogInfomation("Call background service");


                var Esps = await espBackgroundProcessService.GetAll();
                if (client != null && client.IsConnected)
                {
                    // Ngắt kết nối MQTT cũ
                    client.Disconnect();
                }

                while (!stoppingToken.IsCancellationRequested)
                {
                    logger.LogInfomation("Check connection MQTT");
                    if (client == null || !client.IsConnected)
                    {
                        List<string> mqttTopics = new List<string>();
                        List<byte> msgBases = new List<byte>();
                        client = Mqtt.ConnectMQTT("broker.emqx.io", 1883, "abc", "abc", "abc");
                        logger.LogInfomation("Connected to MQTT Broker");
                        foreach (var Esp in Esps)
                        {
                            // client = Mqtt.ConnectMQTT(Esp.MqttServer, Esp.MqttPort, Esp.ClientId, Esp.UserName, Esp.Password);

                            string mqttTopic = $"{Esp.Id}/{Esp.TopicDevice}/{Esp.InstrumentationId}";
                            mqttTopics.Add(mqttTopic);

                            msgBases.Add(MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE);

                            logger.LogInfomation("Sub " + mqttTopic);
                        }
                        Subscribe(client!, mqttTopics.ToArray(), msgBases.ToArray());
                    }
                    if (UploadToMongoDb.isBreak)
                    {
                        logger.LogInfomation("Break + DisConnect mqtt");
                        client?.Disconnect();
                        break;
                    }
                    await Task.Delay(TimeSpan.FromSeconds(5)); // Đợi một khoảng thời gian trước khi kiểm tra lại kết nối.
                }
                client?.Disconnect();
                logger.LogInfomation("Break + DisConnect mqtt");
                await Task.CompletedTask;
            }
            catch
            {
                NotBreak();
                throw;
            }
        }

        private static void NotBreak()
        {
            UploadToMongoDb.isBreak = false;
        }
        private static void MustBreak()
        {
            UploadToMongoDb.isBreak = true;
        }

        private void Subscribe(MqttClient client1, string[] topics, byte[] msgBases)
        {
            client1.MqttMsgPublishReceived += client_MqttMsgPublishReceived;
            client1.Subscribe(topics, msgBases);
        }

        private async void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            try
            {
                string payload = System.Text.Encoding.Default.GetString(e.Message);

                // Create an InstrumentValueByFiveSecondEntity object for the incoming data
                var entity = new InstrumentValueByFiveSecondEntity()
                {
                    PayLoad = payload,
                    Topic = e.Topic,
                    ValueDate = DateTime.Now
                };

                // Add the entity to the list
                messageList.Add(entity);

                // Increment the message count
                messageCount++;

                logger.LogInfomation($"Received `{payload}` from `{e.Topic}` topic");

                if (messageCount >= maxMessageCount)
                {
                    // If the message count reaches the maximum, process and save the data
                    await ProcessAndSaveDataAsync(messageList);

                    // Clear the list and reset the message count
                    messageList.Clear();
                    messageCount = 0;
                }
            }
            catch
            {
                await ProcessAndSaveDataAsync(messageList);
                messageList.Clear();
                messageCount = 0;
                throw;
            }
        }

        private async Task ProcessAndSaveDataAsync(List<InstrumentValueByFiveSecondEntity> data)
        {
            // Process and save the list of data, for example, you can save it to a database
            await dataStatisticsService.PushDatasToDB(data);
        }

        private async Task StopAsync()
        {
            MustBreak();
            await Task.Delay(5010);
            await Task.CompletedTask;
        }

        private async Task StartAsync()
        {
            NotBreak();
            CancellationToken cancellationToken = CancellationToken.None;
            ExecuteAsync(cancellationToken);
            await Task.CompletedTask;
        }

        public async Task RestartJobBackground()
        {
            await StopAsync();
            await StartAsync();
        }
    }
}
