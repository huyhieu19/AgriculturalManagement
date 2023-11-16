using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;

namespace AgriculturalManagement.Controllers.Device
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceControlController : ControllerBase
    {
        private readonly IDeviceControlService _services;

        public DeviceControlController(IDeviceControlService services)
        {
            _services = services;
        }

        [HttpGet]
        public Task<bool> DeviceDriverTurnOff(Guid DeviceId)
        {
            return _services.DeviceDriverTurnOff(DeviceId);
        }
    }
}
