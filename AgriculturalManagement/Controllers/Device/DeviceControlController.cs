using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.DeviceControl;
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

        [HttpPost("OnOffModel")]
        [Authorize(Roles = "Administrator")]
        public Task<bool> DeviceDriverOnOff(OnOffDeviceQueryModel model)
        {
            return _services.DeviceDriverOnOff(model);
        }
        [HttpPost("asyncOnOff")]
        [Authorize(Roles = "Administrator")]
        public Task<bool> AsyncStatusDeviceControl()
        {
            return _services.AsyncStatusDeviceControl();
        }
    }
}
