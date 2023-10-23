using Common;
using Entities;
using Microsoft.Extensions.Hosting;
using MQTTnet;
using MQTTnet.Client;
using Service;
using System.Text;

namespace MQTTProcess
{
    public class UploadToMongoDb : BackgroundService
    {
        private readonly IDataStatisticsService dataStatisticsService;
        //private readonly IMqttClient mqttClient = new MqttFactory().CreateMqttClient();
        private const string mqttServer = "broker.emqx.io";
        private const int mqttPort = 1883;
        private const string mqttTopic = "d6rjcudf7yfrokfyd6w84or994kffef/thisisdeviceid1/R/temperature";

        public UploadToMongoDb(IDataStatisticsService dataStatisticsService)
        {
            this.dataStatisticsService = dataStatisticsService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //await ConnectAsync();
            //await StartSubscriptionAsync();
            // Run every 5 seconds
            while (!stoppingToken.IsCancellationRequested)
            {
                //await ReconnectAsync();

                await Upload();

                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            }
        }

        public async Task Upload()
        {
            var mqttFactory = new MqttFactory();

            using (var mqttClient = mqttFactory.CreateMqttClient())
            {
                var mqttClientOptions = new MqttClientOptionsBuilder().WithTcpServer("broker.emqx.io", 1883).Build();

                await mqttClient.ConnectAsync(mqttClientOptions, CancellationToken.None);

                var mqttSubscribeOptions = mqttFactory.CreateSubscribeOptionsBuilder()
                    .WithTopicFilter(
                        f =>
                        {
                            f.WithTopic(mqttTopic);
                        })
                    .Build();

                await mqttClient.SubscribeAsync(mqttSubscribeOptions, CancellationToken.None);

                Console.WriteLine("MQTT client subscribed to topic.");

                mqttClient.ApplicationMessageReceivedAsync += async (e) =>
                {
                    Console.WriteLine("Received application message.");

                    e.DumpToConsole();

                    // Trích xuất và xử lý dữ liệu từ thông điệp MQTT
                    string receivedData = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);

                    // Hiển thị dữ liệu đã nhận được từ MQTT
                    Console.WriteLine($"Received data: {receivedData}");
                    Console.WriteLine($"Received message on topic {e.ApplicationMessage.Topic}: {e.ApplicationMessage.ConvertPayloadToString()}");

                    await dataStatisticsService.PushDataToDB(new InstrumentValueByFiveSecondEntity()
                    {
                        PayLoad = $"{e.ApplicationMessage.ConvertPayloadToString()}",
                        Topic = $"{mqttTopic}",
                        ValueDate = DateTime.Now,
                    });

                    // Thực hiện xử lý dữ liệu trong ứng dụng của bạn
                    //return Task.CompletedTask;
                    await Task.Delay(1000);
                };
                await Task.Delay(2000);

                //The response contains additional data sent by the server after subscribing.
                //Console.WriteLine(response.DumpToConsole());
            }


            //var options = new MqttClientOptionsBuilder()
            //    .WithTcpServer(mqttServer, mqttPort)
            //    .Build();


            //mqttClient.ApplicationMessageReceived += async (s, e) =>
            //{
            //    Console.WriteLine($"Received message on topic {e.ApplicationMessage.Topic}: {Encoding.UTF8.GetString(e.ApplicationMessage.Payload)}");
            //};

        }

        //public async Task StartSubscriptionAsync()
        //{
        //    Console.WriteLine("MQTT client subscribed to topic.");
        //    var mqttSubscribeOptions = new MqttFactory().CreateSubscribeOptionsBuilder()
        //            .WithTopicFilter(
        //                f =>
        //                {
        //                    f.WithTopic("d6rjcudf7yfrokfyd6w84or994kffef/thisisdeviceid1/R/temperature");
        //                })
        //            .Build();

        //    await mqttClient.SubscribeAsync(mqttSubscribeOptions, CancellationToken.None);
        //}

        //private async Task ConnectAsync()
        //{
        //    try
        //    {
        //        var mqttClientOptions = new MqttClientOptionsBuilder().WithTcpServer(mqttServer, mqttPort).Build();

        //        await mqttClient.ConnectAsync(mqttClientOptions, CancellationToken.None);
        //        Console.WriteLine("MQTT client connected.");
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error connecting to MQTT broker: {ex.Message}");
        //    }
        //}

        //private async Task ReconnectAsync()
        //{
        //    while (!mqttClient.IsConnected)
        //    {
        //        await Task.Delay(5000); // Wait for 5 seconds before reconnecting
        //        var options = new MqttClientOptionsBuilder()
        //            .WithTcpServer(mqttServer, mqttPort)
        //            .Build();
        //        await StartSubscriptionAsync();
        //        await ConnectAsync();
        //    }
        //}
    }
}
