using Common.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.DeviceControl;
using Service.Contracts;
using Service.Contracts.Logger;

namespace AgriculturalManagement.Controllers.Device
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceControlController : ControllerBase
    {
        private readonly IDeviceControlService _services;
        private readonly ILoggerManager _logger;

        public DeviceControlController(IDeviceControlService services, ILoggerManager logger)
        {
            _services = services;
            _logger = logger;
        }

        [HttpPost("OnOffModel")]
        [Authorize(Roles = "Administrator")]
        public async Task<bool> DeviceDriverOnOff(OnOffDeviceQueryModel model)
        {

            var result = await _services.DeviceDriverOnOff(model);
            // ghi lại đóng mở thiết bị (thủ công)
            await _logger.LogOnOffDevice(new Entities.LogProcess.LogDeviceStatusEntity()
            {
                DeviceName = model.DeviceName,
                RequestOn = model.RequestOn,
                TypeOnOff = (int)TypeOnOff.Manual,
                ValueDate = DateTime.UtcNow,
                IsSuccess = result
            });
            return result;
        }
        [HttpPost("asyncOnOff")]
        [Authorize(Roles = "Administrator")]
        public Task<bool> AsyncStatusDeviceControl()
        {
            return _services.AsyncStatusDeviceControl();
        }
    }
}
