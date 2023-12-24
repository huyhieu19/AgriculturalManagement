using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Device;
using Service.Contracts;

namespace AgriculturalManagement.Controllers.Device
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceController : ControllerBase
    {
        private readonly IServiceManager serviceManager;
        public DeviceController(IServiceManager serviceManager)
        {
            this.serviceManager = serviceManager;
        }

        [HttpPost("Info")]
        [Authorize(Roles = "Administrator")]
        public async Task<DeviceInformationDisplayModel> GetInfor([FromBody] InforQuery query)
        {
            return await serviceManager.Device.GetInforDevice(query.deviceId);
        }
    }
    public class InforQuery
    {
        public Guid deviceId { get; set; }
    }
}
