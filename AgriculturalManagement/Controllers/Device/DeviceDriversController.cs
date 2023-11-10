using Microsoft.AspNetCore.Mvc;
using Models;
using Service.Contracts;

namespace AgriculturalManagement.Controllers.Device
{
    [Route("api/DeviceDrivers")]
    [ApiController]
    public class DeviceDriversController : ControllerBase
    {
        private readonly IServiceManager service;
        public DeviceDriversController(IServiceManager service) => this.service = service;

        // Required for UI - Get device driver
        [HttpGet, Route("devicedriverbyzone")]
        public async Task<IEnumerable<DeviceDriverDisplayModel>> GetDeviceDriverByZoneAsync(int Id)
        {
            return await service.DeviceDriver.GetDeviceDriverByZoneAsync(Id);
        }

        /// <summary>
        /// Required for UI - Create device driver
        /// </summary>
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
        [HttpGet, Route("timer/all")]
        public async Task<IEnumerable<TimerDeviceDriverDisplayModel>> GetAllTimer()
        {
            return await service.DeviceDriver.GetAllTimer();
        }
        [HttpPost, Route("timer/databydevice")]
        public async Task<IEnumerable<TimerDeviceDriverDisplayModel>> GetAllTimerByDeviceId(int Id)
        {
            return await service.DeviceDriver.GetAllTimerByDeviceId(Id);
        }
        [HttpPost, Route("timer/device")]
        public async Task<IActionResult> CreateTimer(TimerDeviceDriverCreateModel model)
        {
            await service.DeviceDriver.CreateTimer(model);
            return Ok(true);
        }
        [HttpPut, Route("timer/device")]
        public async Task<IActionResult> UpdateTimer(TimerDeviceDriverDisplayModel model)
        {
            await service.DeviceDriver.UpdateTimer(model);
            return Ok(true);
        }
        [HttpDelete, Route("timer/device")]
        public async Task<IActionResult> DeleteTimer(int Id)
        {
            await service.DeviceDriver.DeleteTimer(Id);
            return Ok(true);
        }
        [HttpGet, Route("timer/HistoryByDevice")]
        public async Task<IEnumerable<TimerDeviceDriverDisplayModel>> GetAllTimerHistoryByDeviceId(int Id)
        {
            return await service.DeviceDriver.GetAllTimerHistoryByDeviceId(Id);
        }
        [HttpGet, Route("timer/AllHistory")]
        public async Task<IEnumerable<TimerDeviceDriverDisplayModel>> GetAllTimerHistory()
        {
            return await service.DeviceDriver.GetAllTimerHistory();
        }
    }
}