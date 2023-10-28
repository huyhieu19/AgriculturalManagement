using uPLibrary.Networking.M2Mqtt;

namespace MQTTProcess
{
    public static class Mqtt
    {
        public static MqttClient ConnectMQTT(string broker, int port, string clientId, string username, string password)
        {
            MqttClient client = new MqttClient(broker, port, false, MqttSslProtocols.None, null, null);
            client.Connect(clientId, username, password);
            return client;
        }
    }
}
