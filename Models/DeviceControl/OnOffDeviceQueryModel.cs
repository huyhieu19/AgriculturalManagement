using Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DeviceControl
{
    public class OnOffDeviceQueryModel
    {
        public Guid DeviceId { get; set; }
        public DeviceType DeviceType { get; set; }
        public string DeviceNameNumber { get; set; } = null!;
        public bool RequestOn { get; set; }
    }
}
