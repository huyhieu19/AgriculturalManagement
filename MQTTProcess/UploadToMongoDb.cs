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
        Task<bool> RestartJobBackground();
    }
    public class UploadToMongoDb : BackgroundService, ICustomServiceStopper
    {
        private readonly ILoggerManager logger;
        private readonly IDataStatisticsService dataStatisticsService;
        private readonly IEspBackgroundProcessService espBackgroundProcessService;
        private static List<InstrumentValueByFiveSecondEntity> messageList = new List<InstrumentValueByFiveSecondEntity>();
        private static int messageCount = 0;
        private const int maxMessageCount = 50;
        private static bool isBreakLoop = false;
        private static bool isContinue = false;
        private static MqttClient? mqttClient;
        private static int countBreak = 0;


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

                logger.LogInformation("Call background service");

                var Esps = await espBackgroundProcessService.GetAll();

                if (mqttClient != null && mqttClient.IsConnected)
                {
                    // Ngắt kết nối MQTT cũ
                    mqttClient.Disconnect();
                }
                SetCountBreakToZero();
                while (!stoppingToken.IsCancellationRequested)
                {
                    if (countBreak >= 500 || countBreak == 0)
                    {
                        logger.LogInformation("Check connection MQTT");
                        if (mqttClient == null || !mqttClient.IsConnected)
                        {
                            mqttClient = Mqtt.ConnectMQTT("broker.emqx.io", 1883, "abc", "abc", "abc");
                            List<string> mqttTopics = new List<string>();
                            List<byte> msgBases = new List<byte>();
                            logger.LogInformation("Connected to MQTT Broker");
                            foreach (var Esp in Esps)
                            {
                                string mqttTopic = $"{Esp.Id}/{Esp.TopicDevice}/{Esp.InstrumentationId}";
                                mqttTopics.Add(mqttTopic);
                                msgBases.Add(MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE);
                                logger.LogInformation("Sub -> " + mqttTopic);
                            }
                            Subscribe(mqttClient!, mqttTopics.ToArray(), msgBases.ToArray());
                        }
                        SetCountBreakToOne();
                    }
                    if (UploadToMongoDb.isBreakLoop)
                    {
                        logger.LogInformation("Break + DisConnect mqtt");
                        mqttClient?.Disconnect();
                        SetIsBreakLoopToFalse();
                        SetContinueToTrue();
                        break;
                    }
                    await Task.Delay(TimeSpan.FromSeconds(0.01));
                    SetCountBreakPlusOne();
                }

            }
            catch
            {
                SetIsBreakLoopToFalse();
                logger.LogWarning("MQTT Subscribe topic or push data have an exception");
                throw;
            }
        }
        private static void SetIsBreakLoopToFalse()
        {
            UploadToMongoDb.isBreakLoop = false;
        }
        private static void SetIsBreakLoopToTrue()
        {
            UploadToMongoDb.isBreakLoop = true;
        }
        private static void SetContinueToTrue()
        {
            UploadToMongoDb.isContinue = true;
        }
        private static void SetCountBreakToZero()
        {
            UploadToMongoDb.countBreak = 0;
        }
        private static void SetCountBreakToOne()
        {
            UploadToMongoDb.countBreak = 1;
        }
        private static void SetCountBreakPlusOne()
        {
            UploadToMongoDb.countBreak += 1;
        }
        private static void SetMessageCountPlusOne()
        {
            UploadToMongoDb.messageCount++;
        }
        private static void SetMessageCountToZero()
        {
            UploadToMongoDb.messageCount = 0;
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
                SetMessageCountPlusOne();

                logger.LogInformation($"Received `{payload}` from `{e.Topic}` topic");

                if (messageCount >= maxMessageCount)
                {
                    // If the message count reaches the maximum, process and save the data
                    await Task.Run(async () =>
                    {
                        await ProcessAndSaveDataAsync(messageList);
                    });
                    // Clear the list and reset the message count
                    messageList.Clear();
                    SetMessageCountToZero();
                }
            }
            catch
            {
                await ProcessAndSaveDataAsync(messageList);
                messageList.Clear();
                SetMessageCountToZero();
                throw;
            }
        }

        private async Task ProcessAndSaveDataAsync(List<InstrumentValueByFiveSecondEntity> data)
        {
            // Process and save the list of data, for example, you can save it to a database
            await dataStatisticsService.PushDatasToDB(data);
        }

        private void StartAsync()
        {
            Mutex mutex = new Mutex();
            mutex.WaitOne();
            CancellationToken cancellationToken = CancellationToken.None;
            while (true)
            {
                if (UploadToMongoDb.isContinue)
                {
                    ExecuteAsync(cancellationToken);
                    break;
                }
            }
            mutex.ReleaseMutex();
        }

        public async Task<bool> RestartJobBackground()
        {
            SetIsBreakLoopToTrue();
            await Task.Delay(TimeSpan.FromSeconds(0.5));
            StartAsync();
            return true;
        }
    }
}
