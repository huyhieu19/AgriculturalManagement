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
        [HttpGet, Route("DeviceDriverByZone")]
        public async Task<IEnumerable<DeviceDriverDisplayModel>> GetDeviceDriverByZoneAsync(int Id)
        {
            return await service.DeviceDriver.GetDeviceDriverByZoneAsync(Id);
        }
    }
}
