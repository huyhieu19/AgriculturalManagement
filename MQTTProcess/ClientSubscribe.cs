﻿//using Common;
//using MQTTnet;
//using MQTTnet.Client;
//using MQTTnet.Packets;
//using MQTTnet.Protocol;
//using System.Text;

//namespace MQTTProcess
//{
//    public static class ClientSubscribe
//    {
//        public static async Task Handle_Received_Application_Message()
//        {
//            /*
//             * This sample subscribes to a topic and processes the received message.
//             */

//            var mqttFactory = new MqttFactory();

//            using (var mqttClient = mqttFactory.CreateMqttClient())
//            {
//                var mqttClientOptions = new MqttClientOptionsBuilder().WithTcpServer("broker.hivemq.com").Build();

//                // Setup message handling before connecting so that queued messages
//                // are also handled properly. When there is no event handler attached all
//                // received messages get lost.
//                mqttClient.ApplicationMessageReceivedAsync += e =>
//                {
//                    Console.WriteLine("Received application message.");
//                    e.DumpToConsole();

//                    return Task.CompletedTask;
//                };

//                await mqttClient.ConnectAsync(mqttClientOptions, CancellationToken.None);

//                var mqttSubscribeOptions = mqttFactory.CreateSubscribeOptionsBuilder()
//                    .WithTopicFilter(
//                        f =>
//                        {
//                            f.WithTopic("mqttnet/samples/topic/2");
//                        })
//                    .Build();

//                await mqttClient.SubscribeAsync(mqttSubscribeOptions, CancellationToken.None);

//                Console.WriteLine("MQTT client subscribed to topic.");

//                Console.WriteLine("Press enter to exit.");
//                Console.ReadLine();
//            }
//        }

//        public static async Task Send_Responses()
//        {
//            /*
//             * This sample subscribes to a topic and sends a response to the broker. This requires at least QoS level 1 to work!
//             */

//            var mqttFactory = new MqttFactory();

//            using (var mqttClient = mqttFactory.CreateMqttClient())
//            {
//                mqttClient.ApplicationMessageReceivedAsync += delegate (MqttApplicationMessageReceivedEventArgs args)
//                {
//                    // Do some work with the message...

//                    // Now respond to the broker with a reason code other than success.
//                    args.ReasonCode = MqttApplicationMessageReceivedReasonCode.ImplementationSpecificError;
//                    args.ResponseReasonString = "That did not work!";

//                    // User properties require MQTT v5!
//                    args.ResponseUserProperties.Add(new MqttUserProperty("My", "Data"));

//                    // Now the broker will resend the message again.
//                    return Task.CompletedTask;
//                };

//                var mqttClientOptions = new MqttClientOptionsBuilder().WithTcpServer("broker.hivemq.com").Build();

//                await mqttClient.ConnectAsync(mqttClientOptions, CancellationToken.None);

//                var mqttSubscribeOptions = mqttFactory.CreateSubscribeOptionsBuilder()
//                    .WithTopicFilter(
//                        f =>
//                        {
//                            f.WithTopic("mqttnet/samples/topic/1");
//                        })
//                    .Build();

//                var response = await mqttClient.SubscribeAsync(mqttSubscribeOptions, CancellationToken.None);

//                Console.WriteLine("MQTT client subscribed to topic.");

//                // The response contains additional data sent by the server after subscribing.
//                response.DumpToConsole();
//            }
//        }

//        public static async Task Subscribe_Multiple_Topics()
//        {
//            /*
//             * This sample subscribes to several topics in a single request.
//             */

//            var mqttFactory = new MqttFactory();

//            using (var mqttClient = mqttFactory.CreateMqttClient())
//            {
//                var mqttClientOptions = new MqttClientOptionsBuilder().WithTcpServer("broker.hivemq.com").Build();

//                await mqttClient.ConnectAsync(mqttClientOptions, CancellationToken.None);

//                // Create the subscribe options including several topics with different options.
//                // It is also possible to all of these topics using a dedicated call of _SubscribeAsync_ per topic.
//                var mqttSubscribeOptions = mqttFactory.CreateSubscribeOptionsBuilder()
//                    .WithTopicFilter(
//                        f =>
//                        {
//                            f.WithTopic("mqttnet/samples/topic/1");
//                        })
//                    .WithTopicFilter(
//                        f =>
//                        {
//                            f.WithTopic("mqttnet/samples/topic/2").WithNoLocal();
//                        })
//                    .WithTopicFilter(
//                        f =>
//                        {
//                            f.WithTopic("mqttnet/samples/topic/3").WithRetainHandling(MqttRetainHandling.SendAtSubscribe);
//                        })
//                    .Build();

//                var response = await mqttClient.SubscribeAsync(mqttSubscribeOptions, CancellationToken.None);
//                Console.WriteLine("MQTT client subscribed to topics.");

//                // The response contains additional data sent by the server after subscribing.
//                response.DumpToConsole();
//            }
//        }

//        public static async Task Subscribe_Topic()
//        {
//            /*
//             * This sample subscribes to a topic.
//             */

//            var mqttFactory = new MqttFactory();

//            using (var mqttClient = mqttFactory.CreateMqttClient())
//            {
//                var mqttClientOptions = new MqttClientOptionsBuilder().WithTcpServer("broker.emqx.io", 1883).Build();

//                await mqttClient.ConnectAsync(mqttClientOptions, CancellationToken.None);

//                var mqttSubscribeOptions = mqttFactory.CreateSubscribeOptionsBuilder()
//                    .WithTopicFilter(
//                        f =>
//                        {
//                            f.WithTopic("d6rjcudf7yfrokfyd6w84or994kffef/thisisdeviceid1/R/temperature");
//                        })
//                    .Build();

//                var response = await mqttClient.SubscribeAsync(mqttSubscribeOptions, CancellationToken.None);

//                Console.WriteLine("MQTT client subscribed to topic.");

//                mqttClient.ApplicationMessageReceivedAsync += e =>
//                {
//                    Console.WriteLine("Received application message.");

//                    e.DumpToConsole();

//                    // Trích xuất và xử lý dữ liệu từ thông điệp MQTT
//                    string receivedData = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);

//                    // Hiển thị dữ liệu đã nhận được từ MQTT
//                    Console.WriteLine($"Received data: {receivedData}");
//                    Console.WriteLine($"Received message on topic {e.ApplicationMessage.Topic}: {e.ApplicationMessage.ConvertPayloadToString()}");
//                    // Thực hiện xử lý dữ liệu trong ứng dụng của bạn
//                    return Task.CompletedTask;
//                };

//                // The response contains additional data sent by the server after subscribing.
//                Console.WriteLine(response.DumpToConsole());
//            }
//        }
//    }
//}
