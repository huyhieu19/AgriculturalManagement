//using Common;
//using MQTTnet;
//using MQTTnet.Client;
//using MQTTnet.Extensions.WebSocket4Net;
//using MQTTnet.Formatter;
//using System.Security.Authentication;

//namespace MQTTProcess
//{
//    public static class ClientConnection
//    {

//        public static async Task Clean_Disconnect()
//        {
//            /*
//             * This sample disconnects in a clean way. This will send a MQTT DISCONNECT packet
//             * to the server and close the connection afterwards.
//             *
//             * See sample _Connect_Client_ for more details.
//             */

//            var mqttFactory = new MqttFactory();

//            using (var mqttClient = mqttFactory.CreateMqttClient())
//            {
//                var mqttClientOptions = new MqttClientOptionsBuilder().WithTcpServer("broker.hivemq.com").Build();
//                await mqttClient.ConnectAsync(mqttClientOptions, CancellationToken.None);

//                // This will send the DISCONNECT packet. Calling _Dispose_ without DisconnectAsync the 
//                // connection is closed in a "not clean" way. See MQTT specification for more details.
//                await mqttClient.DisconnectAsync(new MqttClientDisconnectOptionsBuilder().WithReason(MqttClientDisconnectOptionsReason.NormalDisconnection).Build());
//            }
//        }


//        public static async Task Connect_Client()
//        {
//            /*
//             * This sample creates a simple MQTT client and connects to a public broker.
//             *
//             * Always dispose the client when it is no longer used.
//             * The default version of MQTT is 3.1.1.
//             */

//            var mqttFactory = new MqttFactory();

//            using (var mqttClient = mqttFactory.CreateMqttClient())
//            {
//                // Use builder classes where possible in this project.
//                var mqttClientOptions = new MqttClientOptionsBuilder().WithTcpServer("broker.emqx.io", 1883).Build();

//                // This will throw an exception if the server is not available.
//                // The result from this message returns additional data which was sent 
//                // from the server. Please refer to the MQTT protocol specification for details.
//                var response = await mqttClient.ConnectAsync(mqttClientOptions, CancellationToken.None);

//                Console.WriteLine("The MQTT client is connected.");

//                response.DumpToConsole();

//                // Send a clean disconnect to the server by calling _DisconnectAsync_. Without this the TCP connection
//                // gets dropped and the server will handle this as a non clean disconnect (see MQTT spec for details).
//                var mqttClientDisconnectOptions = mqttFactory.CreateClientDisconnectOptionsBuilder().Build();

//                await mqttClient.DisconnectAsync(mqttClientDisconnectOptions, CancellationToken.None);
//            }

//        }
//        public static async Task Connect_Client_Timeout()
//        {
//            /*
//             * This sample creates a simple MQTT client and connects to an invalid broker using a timeout.
//             * 
//             * This is a modified version of the sample _Connect_Client_! See other sample for more details.
//             */

//            var mqttFactory = new MqttFactory();

//            using (var mqttClient = mqttFactory.CreateMqttClient())
//            {
//                var mqttClientOptions = new MqttClientOptionsBuilder().WithTcpServer("127.0.0.1").Build();

//                try
//                {
//                    using (var timeoutToken = new CancellationTokenSource(TimeSpan.FromSeconds(1)))
//                    {
//                        await mqttClient.ConnectAsync(mqttClientOptions, timeoutToken.Token);
//                    }
//                }
//                catch (OperationCanceledException)
//                {
//                    Console.WriteLine("Timeout while connecting.");
//                }
//            }
//        }

//        public static async Task Connect_Client_Using_MQTTv5()
//        {
//            /*
//             * This sample creates a simple MQTT client and connects to a public broker using MQTTv5.
//             * 
//             * This is a modified version of the sample _Connect_Client_! See other sample for more details.
//             */

//            var mqttFactory = new MqttFactory();

//            using (var mqttClient = mqttFactory.CreateMqttClient())
//            {
//                var mqttClientOptions = new MqttClientOptionsBuilder().WithTcpServer("broker.hivemq.com").WithProtocolVersion(MqttProtocolVersion.V500).Build();

//                // In MQTTv5 the response contains much more information.
//                var response = await mqttClient.ConnectAsync(mqttClientOptions, CancellationToken.None);

//                Console.WriteLine("The MQTT client is connected.");

//                response.DumpToConsole();
//            }
//        }
//        public static async Task Disconnect_Clean()
//        {
//            /*
//             * This sample disconnects from the server with sending a DISCONNECT packet.
//             * This way of disconnecting is treated as a clean disconnect which will not
//             * trigger sending the last will etc.
//             */

//            var mqttFactory = new MqttFactory();

//            using (var mqttClient = mqttFactory.CreateMqttClient())
//            {
//                var mqttClientOptions = new MqttClientOptionsBuilder().WithTcpServer("broker.hivemq.com").Build();

//                await mqttClient.ConnectAsync(mqttClientOptions, CancellationToken.None);

//                // Calling _DisconnectAsync_ will send a DISCONNECT packet before closing the connection.
//                // Using a reason code requires MQTT version 5.0.0!
//                await mqttClient.DisconnectAsync(MqttClientDisconnectOptionsReason.ImplementationSpecificError);
//            }
//        }
//        public static async Task Connect_Client_With_TLS_Encryption()
//        {
//            /*
//             * This sample creates a simple MQTT client and connects to a public broker with enabled TLS encryption.
//             * 
//             * This is a modified version of the sample _Connect_Client_! See other sample for more details.
//             */

//            var mqttFactory = new MqttFactory();

//            using (var mqttClient = mqttFactory.CreateMqttClient())
//            {
//                var mqttClientOptions = new MqttClientOptionsBuilder().WithTcpServer("test.mosquitto.org", 8883)
//                    .WithTlsOptions(
//                        o => o.WithCertificateValidationHandler(
//                            // The used public broker sometimes has invalid certificates. This sample accepts all
//                            // certificates. This should not be used in live environments.
//                            _ => true))
//                    .Build();

//                // In MQTTv5 the response contains much more information.
//                using (var timeout = new CancellationTokenSource(5000))
//                {
//                    var response = await mqttClient.ConnectAsync(mqttClientOptions, timeout.Token);

//                    Console.WriteLine("The MQTT client is connected.");

//                    response.DumpToConsole();
//                }
//            }
//        }
//        public static async Task Connect_Client_Using_TLS_1_2()
//        {
//            /*
//             * This sample creates a simple MQTT client and connects to a public broker using TLS 1.2 encryption.
//             * 
//             * This is a modified version of the sample _Connect_Client_! See other sample for more details.
//             */

//            var mqttFactory = new MqttFactory();

//            using (var mqttClient = mqttFactory.CreateMqttClient())
//            {
//                var mqttClientOptions = new MqttClientOptionsBuilder().WithTcpServer("mqtt.fluux.io")
//                    .WithTlsOptions(
//                        o =>
//                        {
//                            // The used public broker sometimes has invalid certificates. This sample accepts all
//                            // certificates. This should not be used in live environments.
//                            o.WithCertificateValidationHandler(_ => true);

//                            // The default value is determined by the OS. Set manually to force version.
//                            o.WithSslProtocols(SslProtocols.Tls12);
//                        })
//                    .Build();

//                using (var timeout = new CancellationTokenSource(5000))
//                {
//                    await mqttClient.ConnectAsync(mqttClientOptions, timeout.Token);

//                    Console.WriteLine("The MQTT client is connected.");
//                }
//            }
//        }

//        public static async Task Connect_Client_Using_WebSocket4Net()
//        {
//            /*
//             * This sample creates a simple MQTT client and connects to a public broker using a WebSocket connection.
//             * Instead of the .NET implementation of WebSockets the implementation from WebSocket4Net is used. It provides more
//             * encryption algorithms and supports more platforms.
//             * 
//             * This is a modified version of the sample _Connect_Client_! See other sample for more details.
//             */

//            var mqttFactory = new MqttFactory().UseWebSocket4Net();

//            using (var mqttClient = mqttFactory.CreateMqttClient())
//            {
//                var mqttClientOptions = new MqttClientOptionsBuilder().WithWebSocketServer(o => o.WithUri("broker.hivemq.com:8000/mqtt")).Build();

//                var response = await mqttClient.ConnectAsync(mqttClientOptions, CancellationToken.None);

//                Console.WriteLine("The MQTT client is connected.");

//                response.DumpToConsole();
//            }
//        }

//        public static async Task Connect_Client_Using_WebSockets()
//        {
//            /*
//             * This sample creates a simple MQTT client and connects to a public broker using a WebSocket connection.
//             * 
//             * This is a modified version of the sample _Connect_Client_! See other sample for more details.
//             */

//            var mqttFactory = new MqttFactory();

//            using (var mqttClient = mqttFactory.CreateMqttClient())
//            {
//                var mqttClientOptions = new MqttClientOptionsBuilder().WithWebSocketServer(o => o.WithUri("broker.hivemq.com:8000/mqtt")).Build();

//                var response = await mqttClient.ConnectAsync(mqttClientOptions, CancellationToken.None);

//                Console.WriteLine("The MQTT client is connected.");

//                response.DumpToConsole();
//            }
//        }
//    }
//}