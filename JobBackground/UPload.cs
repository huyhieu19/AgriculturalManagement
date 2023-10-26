using Entities;
using Microsoft.Extensions.Options;
using Models;
using MongoDB.Driver;
using MQTTProcess;
using Quartz;
using Service.Contracts.ESP;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace JobBackground
{
    public class UPload : IJob
    {
        private MqttClient mqttclient;
        private List<InstrumentValueByFiveSecondEntity> messageList = new List<InstrumentValueByFiveSecondEntity>();
        private int messageCount = 0;
        private const int maxMessageCount = 50;
        private readonly IEspBackgroundProcessService espBackgroundProcessService;

        private readonly IMongoCollection<InstrumentValueByFiveSecondEntity> instrumentValue;
        private readonly MongoClient mongoclient;

        public UPload(IEspBackgroundProcessService espBackgroundProcessService, IOptions<MongoDbConfig> mongoDbConfig)
        {
            this.espBackgroundProcessService = espBackgroundProcessService;

            this.mongoclient = new MongoClient(mongoDbConfig.Value.ConnectionString);
            var database = this.mongoclient.GetDatabase(mongoDbConfig.Value.DatabaseName);
            instrumentValue = database.GetCollection<InstrumentValueByFiveSecondEntity>(mongoDbConfig.Value.CollectionName);
        }

        public async Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine($"Job executed at {DateTime.Now}");

            Console.WriteLine("Check connection MQTT");


            //var Esps = await espBackgroundProcessService.GetAll();

            if (mqttclient == null || !mqttclient.IsConnected)
            {
                List<string> mqttTopics = new List<string>() { "anc" };
                List<byte> msgBases = new List<byte>();
                mqttclient = Mqtt.ConnectMQTT("broker.emqx.io", 1883, "abc", "abc", "abc");
                //foreach (var Esp in Esps)
                //{
                //    // client = Mqtt.ConnectMQTT(Esp.MqttServer, Esp.MqttPort, Esp.ClientId, Esp.UserName, Esp.Password);

                //    string mqttTopic = $"{Esp.Id}/{Esp.TopicDevice}/{Esp.InstrumentationId}";
                //    mqttTopics.Add(mqttTopic);

                //    msgBases.Add(MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE);

                //    Console.WriteLine("Sub " + mqttTopic);
                //}
                Subscribe(mqttclient!, mqttTopics.ToArray(), msgBases.ToArray());
            }
        }
        private void Subscribe(MqttClient client1, string[] topics, byte[] msgBases)
        {
            //client1.MqttMsgPublishReceived += client_MqttMsgPublishReceived;

            client1.MqttMsgPublishReceived += client_MqttMsgPublishReceived;
            client1.Subscribe(topics, msgBases);
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
            //await dataStatisticsService.PushDatasToDB(data);

            await instrumentValue.InsertManyAsync(data);
        }
    }

    //internal sealed class IntegrationJobFactory : IJobFactory
    //{
    //    private readonly IUnityContainer _container;

    //    public IntegrationJobFactory(IUnityContainer container)
    //    {
    //        _container = container;
    //    }

    //    public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
    //    {
    //        var jobDetail = bundle.JobDetail;
    //        var job = (IJob)_container.Resolve(jobDetail.JobType);
    //        return job;
    //    }

    //    public void ReturnJob(IJob job)
    //    {
    //    }
    //}
}
