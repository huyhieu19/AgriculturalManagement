using Microsoft.AspNetCore.Mvc;
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

    }
}
