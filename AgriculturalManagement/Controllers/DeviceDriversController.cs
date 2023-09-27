using Microsoft.AspNetCore.Mvc;
using Models;
using Service.Contracts;

namespace AgriculturalManagement.Controllers
{
    [Route("api/DeviceDrivers")]
    [ApiController]
    public class DeviceDriversController : ControllerBase
    {
        private readonly IServiceManager service;
        public DeviceDriversController(IServiceManager service) => this.service = service;
        [HttpGet, Route("devicedriverbyzone")]
        public async Task<IEnumerable<DeviceDriverDisplayModel>> GetDeviceDriverByZoneAsync(int Id)
        {
            return await service.DeviceDriver.GetDeviceDriverByZoneAsync(Id);
        }
        [HttpPost, Route("devicedriver")]
        public async Task<IActionResult> CreateDeviceDriver(DeviceDriverCreateModel createModel)
        {
            await service.DeviceDriver.CreateDeviceDriver(createModel);
            return Ok(true);
        }
        [HttpDelete, Route("devicedriver")]
        public async Task<IActionResult> DeleteDeviceDriver(int Id)
        {
            await service.DeviceDriver.DeleteDeviceDriver(Id);
            return Ok(true);
        }
        [HttpGet, Route("devicedrivernotinzone")]
        public async Task<IEnumerable<DeviceDriverDisplayModel>> GetDeviceDriverNotInZoneAsync()
        {
            return await service.DeviceDriver.GetDeviceDriverNotInZoneAsync();
        }
        [HttpDelete, Route("removedevicedriverformzone")]
        public async Task<IActionResult> RemoveDeviceDriver(int Id)
        {
            await service.DeviceDriver.RemoveDeviceDriver(Id);
            return Ok(true);
        }
        [HttpPut, Route("infordevicedriver")]
        public async Task<IActionResult> UpdateInforDeviceDriver([FromBody] DeviceDriverUpdateModel updateModel)
        {
            await service.DeviceDriver.UpdateInforDeviceDriver(updateModel);
            return Ok(true);
        }
    }
}
