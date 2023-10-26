using JobBackground;
using Microsoft.AspNetCore.Mvc;
using Models;
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
        private readonly IDataStatisticsService dataStatisticsService;
        private readonly IEspBackgroundProcessService espBackgroundProcessService;
        ICustomServiceStopper _stopper;
        public EspController(IServiceManager serviceManager
            , IDataStatisticsService dataStatisticsService
            , IEspBackgroundProcessService espBackgroundProcessService,
            ICustomServiceStopper stopper)
        {
            _serviceManager = serviceManager;
            this.dataStatisticsService = dataStatisticsService;
            this.espBackgroundProcessService = espBackgroundProcessService;
            _stopper = stopper;
        }

        [HttpGet("esps")]
        public async Task<List<EspDisplayModel>> GetAll() => await _serviceManager.EspService.GetAll();

        [HttpPost("esp")]
        public async Task<ActionResult<bool>> Create(EspCreateModel model)
        {
            await _serviceManager.EspService.CreateEsp(model);
            return Ok(true);
        }
        [HttpGet("Start")]
        public void Start()
        {
            new JobSchedulerDeviceDriver().ScheduleJobs1();
        }
        [HttpGet("Stop")]
        public void Stop()
        {
            new JobSchedulerDeviceDriver().DeleteScheduleJobs1();
        }

        //[HttpPost]
        //public async Task<IActionResult> Stop()
        //{
        //    await _stopper.StopAsync();
        //    return Ok();
        //}

    }
}
