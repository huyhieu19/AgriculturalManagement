using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Device;
using MQTTProcess;
using Service;
using Service.Contracts;
using Service.Contracts.ESP;

namespace AgriculturalManagement.Controllers.ESP
{
    [Route("api/[controller]")]
    [ApiController]
    public class EspController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IDataStatisticsService dataStatisticsService;
        private readonly IEspBackgroundProcessService espBackgroundProcessService;
        private readonly IRestartAsyncMQTTService customServiceStopper;
        public EspController(IServiceManager serviceManager
            , IDataStatisticsService dataStatisticsService
            , IEspBackgroundProcessService espBackgroundProcessService,
            IRestartAsyncMQTTService customServiceStopper,
            IHttpContextAccessor contextAccessor)
        {
            _serviceManager = serviceManager;
            this.dataStatisticsService = dataStatisticsService;
            this.espBackgroundProcessService = espBackgroundProcessService;
            this.customServiceStopper = customServiceStopper;
            _contextAccessor = contextAccessor;
        }
        [HttpGet("esps-all")]
        public async Task<List<EspDisplayModel>> GetEspsAll()
        {
            return await _serviceManager.EspService.GetEspsAll();
        }

        [HttpGet("esps")]
        public async Task<List<EspDisplayModel>> GetAll()
        {
            var id = _contextAccessor.HttpContext!.User.FindFirst("Id")!.Value;
            return await _serviceManager.EspService.GetEsps(id);
        }

        [HttpPost("esp")]
        public async Task<ActionResult<bool>> Create(EspCreateModel model)
        {
            return await _serviceManager.EspService.CreateEsp(model);
        }

        [HttpPost("user-esp")]
        public async Task<bool> AddEspToUser(Guid espId)
        {
            var userId = _contextAccessor.HttpContext!.User.FindFirst("Id")!.Value;
            return await _serviceManager.EspService.AddEspToUser(espId, userId);
        }

        [HttpDelete("esp")]
        public async Task<bool> Delete(Guid id) => await _serviceManager.EspService.DeleteESP(id);


        [HttpPost("devices-Esp")]
        public async Task<List<DeviceDisplayModel>> DeviceESPDisplay(Guid id)
        {
            return await _serviceManager.EspService.DeviceESPDisplay(id);
        }

        [HttpPost("device-Esp-create")]
        public async Task<bool> DeviceESPCreate(DeviceCreateModel model)
        {
            return await _serviceManager.EspService.DeviceESPCreate(model);
        }

        [HttpPost("device-Esp-delete")]
        public async Task<bool> DeviceESPRemove(Guid id)
        {
            return await _serviceManager.EspService.DeviceESPRemove(id);
        }

    }
}