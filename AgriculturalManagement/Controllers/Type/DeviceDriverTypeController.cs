using Microsoft.AspNetCore.Mvc;
using Models;
using Service.Contracts;

namespace AgriculturalManagement.Controllers.Type
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceDriverTypeController : ControllerBase
    {
        private readonly IServiceManager service;

        public DeviceDriverTypeController(IServiceManager service)
        {
            this.service = service;
        }
        [HttpGet, Route("DeviceDriverTypes")]
        public async Task<List<DeviceDriversTypeDisplayModel>> Get()
        {
            return await service.ValueType.GetTypeDeviceDrivers();
        }
        [HttpPost, Route("DeviceDriverType")]
        public async Task<bool> Create(DeviceDriversTypeCreateModel model)
        {
            return await service.ValueType.CreateTypeDeviceDrivers(model);
        }
        [HttpPut, Route("DeviceDriverType")]
        public async Task<bool> Update(DeviceDriversTypeDisplayModel model)
        {
            return await service.ValueType.UpdateTypeDeviceDriver(model);
        }
        [HttpDelete, Route("DeviceDriverType")]
        public async Task<bool> Delete(DeviceDriversTypeDisplayModel model)
        {
            return await service.ValueType.DeleteTypeDeviceDrivers(model);
        }
    }
}
