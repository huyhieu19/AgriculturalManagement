using Entities;
using Microsoft.Extensions.Hosting;
using Service;
using Service.Contracts.ESP;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace MQTTProcess
{
    public class UploadToMongoDb : BackgroundService
    {
        private readonly IDataStatisticsService dataStatisticsService;
        private readonly IEspBackgroundProcessService espBackgroundProcessService;
        private List<InstrumentValueByFiveSecondEntity> messageList = new List<InstrumentValueByFiveSecondEntity>();
        private int messageCount = 0;
        private const int maxMessageCount = 50;

        public UploadToMongoDb(IDataStatisticsService dataStatisticsService, IEspBackgroundProcessService espBackgroundProcessService)
        {
            this.dataStatisticsService = dataStatisticsService;
            this.espBackgroundProcessService = espBackgroundProcessService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            MqttClient? client = null;
            var Esps = await espBackgroundProcessService.GetAll();


            while (!stoppingToken.IsCancellationRequested)
            {
                if (client == null || !client.IsConnected)
                {
                    List<string> mqttTopics = new List<string>();
                    List<byte> msgBases = new List<byte>();
                    foreach (var Esp in Esps)
                    {
                        client = Mqtt.ConnectMQTT(Esp.MqttServer, Esp.MqttPort, Esp.ClientId, Esp.UserName, Esp.Password);

                        string mqttTopic = $"{Esp.Id}/{Esp.TopicDevice}/{Esp.InstrumentationId}";
                        mqttTopics.Add(mqttTopic);

                        msgBases.Add(MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE);

                        Console.WriteLine("Sub " + mqttTopic);
                    }
                    Subscribe(client!, mqttTopics.ToArray(), msgBases.ToArray());
                }


                await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken); // Đợi một khoảng thời gian trước khi kiểm tra lại kết nối.
            }
        }

        //private void Subscribe(MqttClient client, string[] topics, byte[] msgBases)
        //{
        //    client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;
        //    client.Subscribe(topics, msgBases);
        //}
        //private async void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        //{
        //    string payload = System.Text.Encoding.Default.GetString(e.Message);
        //    await Task.Factory.StartNew(async () =>
        //    {
        //        await dataStatisticsService.PushDataToDB(new InstrumentValueByFiveSecondEntity()
        //        {
        //            PayLoad = $"{payload}",
        //            Topic = $"{e.Topic}",
        //            ValueDate = DateTime.Now,
        //        });
        //    });
        //    Console.WriteLine("Received `{0}` from `{1}` topic", payload, e.Topic);
        //}
        private void Subscribe(MqttClient client, string[] topics, byte[] msgBases)
        {
            client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;
            client.Subscribe(topics, msgBases);
        }

        private async void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
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

            Console.WriteLine($"Received `{payload}` from `{e.Topic}` topic");

            if (messageCount >= maxMessageCount)
            {
                // If the message count reaches the maximum, process and save the data
                await ProcessAndSaveDataAsync(messageList);

                // Clear the list and reset the message count
                messageList.Clear();
                messageCount = 0;
            }
        }

        private async Task ProcessAndSaveDataAsync(List<InstrumentValueByFiveSecondEntity> data)
        {
            // Process and save the list of data, for example, you can save it to a database
            await dataStatisticsService.PushDatasToDB(data);
        }
    }
}
