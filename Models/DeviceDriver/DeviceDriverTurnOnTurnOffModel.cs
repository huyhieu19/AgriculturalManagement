using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DeviceDriver
{
    public class DeviceDriverTurnOnTurnOffModel
    {
        public int Id { get; set; }
        public bool? IsDaily { get; set; } = false;
        public bool? IsAuto { get; set; } = false;
        public int? ShutDownTime { get; set; }
        public int? OpenTimer { get; set; }
        public bool? IsAction { get; set; } = false;
        public bool? IsProblem { get; set; } = false;
    }
}
