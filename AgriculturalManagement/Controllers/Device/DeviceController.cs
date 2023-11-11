using Microsoft.AspNetCore.Mvc;
using Models.Device;
using Service.Contracts;

namespace AgriculturalManagement.Controllers.Device
{
    [Route("api/Device")]
    [ApiController]
    public class DeviceController : ControllerBase
    {
        private readonly IServiceManager serviceManager;

        public DeviceController(IServiceManager serviceManager)
        {
            this.serviceManager = serviceManager;
        }

        [HttpPost("data-table")]
        public async Task<List<DeviceDisplayModel>> DevicesDisplay(int zoneId)
        {
            var response = await serviceManager.Device.DevicesDisplay(zoneId);
            return response;
        }
        [HttpPost("device-add")]
        public async Task<DeviceModifyResponseModel> DeviceCreate(Guid deviceId, int zoneId)
        {
            var response = await serviceManager.Device.DeviceCreate(deviceId, zoneId);
            return response;
        }
        [HttpPost("device-remove")]
        public async Task<DeviceModifyResponseModel> DeviceRemove(Guid deviceId, int zoneId)
        {
            var response = await serviceManager.Device.DeviceRemove(deviceId, zoneId);
            return response;
        }

    }
}
