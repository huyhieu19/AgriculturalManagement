using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Config.Mqtt
{
    public class MqttConnectionConfigModel
    {
        public string SystemId { get; set; } = null!;
        public string ServerName { get; set; } = null!;
        public int Port { get; set; }
        public string ClientId { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string UserPW { get; set; } = null!;
    }
}
