using Entities;
using Microsoft.Extensions.Hosting;
using Service;
using Service.Contracts.Logger;
using Service.Contracts.Module;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace MQTTProcess
{
    public interface IRestartAsyncMQTTService
    {
        Task<bool> RestartJobBackground();
    }
    public class ProcessDataReceivedFromMQTT : BackgroundService, IRestartAsyncMQTTService
    {
        private readonly ILoggerManager logger;
        private readonly IDataStatisticsService dataStatisticsService;
        private readonly IEspBackgroundProcessService espBackgroundProcessService;
        private static List<InstrumentValueByFiveSecondEntity> _messageList = new List<InstrumentValueByFiveSecondEntity>();
        private static int _messageCount = 0;
        private const int MaxMessageCount = 50;
        private static bool _isBreakLoop = false;
        private static bool _isContinue = false;
        private MqttClient? mqttClient;
        private static int _countBreak = 0;


        public ProcessDataReceivedFromMQTT(IDataStatisticsService dataStatisticsService,
            IEspBackgroundProcessService espBackgroundProcessService,
            ILoggerManager logger)
        {
            this.dataStatisticsService = dataStatisticsService;
            this.espBackgroundProcessService = espBackgroundProcessService;
            this.logger = logger;
        }

        public async Task<bool> RestartJobBackground()
        {
            SetIsBreakLoopToTrue();
            await Task.Delay(TimeSpan.FromSeconds(0.5));
            StartAsync();
            return true;
        }

        // Run background process Connecting, Subscribe and Receiving data from MQTT
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {

                logger.LogInformation("Call background service");

                var Esps = await espBackgroundProcessService.GetAll();

                if (Esps.Any())
                {
                    if (mqttClient != null && mqttClient.IsConnected)
                    {
                        // Ngắt kết nối MQTT cũ
                        mqttClient.Disconnect();
                    }
                    SetCountBreakToZero();
                    while (!stoppingToken.IsCancellationRequested)
                    {
                        if (_countBreak >= 50)
                        {
                            logger.LogInformation("Check connection MQTT");
                            if (mqttClient == null || !mqttClient.IsConnected)
                            {
                                mqttClient = Mqtt.ConnectMQTT("broker.emqx.io", 1883, "abc", "abc", "abc");
                                List<string> mqttTopics = new();
                                List<byte> msgBases = new();
                                logger.LogInformation("Connected to MQTT Broker");
                                foreach (var esp in Esps)
                                {
                                    string mqttTopic = $"{esp.Id}/{esp.TopicDevice}/{esp.InstrumentationId}";
                                    mqttTopics.Add(mqttTopic);
                                    msgBases.Add(MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE);
                                    logger.LogInformation("Sub -> " + mqttTopic);
                                }
                                Subscribe(mqttClient!, mqttTopics.ToArray(), msgBases.ToArray());
                            }
                            SetCountBreakToOne();
                        }
                        if (ProcessDataReceivedFromMQTT._isBreakLoop)
                        {
                            logger.LogInformation("Break + DisConnect mqtt");
                            mqttClient?.Disconnect();
                            SetIsBreakLoopToFalse();
                            SetContinueToTrue();
                            break;
                        }
                        await Task.Delay(TimeSpan.FromSeconds(0.1));
                        SetCountBreakPlusOne();
                    }
                }
            }
            catch
            {
                SetIsBreakLoopToFalse();
                logger.LogWarning("MQTT Subscribe topic or push data have an exception");
                throw;
            }
        }

        // Subscribe to receive events from topics via public MQTT protocol
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

            // Add many process working with topic
            Task.Run(async () =>
            {
                await ProcessAndSaveDataAsync(entity);
            });



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

        private async Task ProcessOpensOrClosesDeviceAccordingToThresholdValue(InstrumentValueByFiveSecondEntity entity)
        {

        }

        private void StartAsync()
        {
            Mutex mutex = new Mutex();
            mutex.WaitOne();
            CancellationToken cancellationToken = CancellationToken.None;
            while (true)
            {
                if (ProcessDataReceivedFromMQTT._isContinue)
                {
                    ExecuteAsync(cancellationToken);
                    break;
                }
            }
            SetContinueToFalse();
            mutex.ReleaseMutex();
        }

        #region Function that handles static variables

        private static void SetIsBreakLoopToFalse()
        {
            ProcessDataReceivedFromMQTT._isBreakLoop = false;
        }
        private static void SetIsBreakLoopToTrue()
        {
            ProcessDataReceivedFromMQTT._isBreakLoop = true;
        }

        private static void SetContinueToTrue()
        {
            ProcessDataReceivedFromMQTT._isContinue = true;
        }
        private static void SetContinueToFalse()
        {
            ProcessDataReceivedFromMQTT._isContinue = false;
        }

        private static void SetCountBreakToZero()
        {
            ProcessDataReceivedFromMQTT._countBreak = 0;
        }
        private static void SetCountBreakToOne()
        {
            ProcessDataReceivedFromMQTT._countBreak = 1;
        }
        private static void SetCountBreakPlusOne()
        {
            ProcessDataReceivedFromMQTT._countBreak += 1;
        }

        private static void SetMessageCountPlusOne()
        {
            ProcessDataReceivedFromMQTT._messageCount++;
        }
        private static void SetMessageCountToZero()
        {
            ProcessDataReceivedFromMQTT._messageCount = 0;
        }
        #endregion
    }
}