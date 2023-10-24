using Microsoft.AspNetCore.Mvc;
using Models;
using MQTTProcess;
using Service.Contracts;

namespace AgriculturalManagement.Controllers.ESP
{
    [Route("api/[controller]")]
    [ApiController]
    public class EspController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public EspController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet("esps")]
        public async Task<List<EspDisplayModel>> GetAll() => await _serviceManager.EspService.GetAll();

        [HttpPost("esp")]
        public async Task<ActionResult<bool>> Create(EspCreateModel model)
        {
            await _serviceManager.EspService.CreateEsp(model);
            return Ok(true);
        }
        [HttpGet("test")]
        public async Task Subscribe_Topic() => await ClientSubscribe.Subscribe_Topic();


    }
}
