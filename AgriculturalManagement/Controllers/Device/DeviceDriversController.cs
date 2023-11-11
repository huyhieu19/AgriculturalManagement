using Microsoft.AspNetCore.Mvc;
using Models.DeviceTimer;
using Service.Contracts;

namespace AgriculturalManagement.Controllers.Device
{
    [Route("api/DeviceDrivers")]
    [ApiController]
    public class DeviceDriversController : ControllerBase
    {
        private readonly IServiceManager service;
        public DeviceDriversController(IServiceManager service) => this.service = service;

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