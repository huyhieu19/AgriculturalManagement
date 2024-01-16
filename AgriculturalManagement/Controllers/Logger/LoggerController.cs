using Entities;
using Entities.LogProcess;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.DeviceData;
using Models.LoggerProcess;
using Service;

namespace AgriculturalManagement.Controllers.Logger
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoggerController : ControllerBase
    {
        private readonly IDataStatisticsService service;

        public LoggerController(IDataStatisticsService service)
        {
            this.service = service;
        }

        [HttpPost("logsystem")]
        public async Task<BaseResModel<LogProcessEntity>> LogProcess([FromBody] LoggerProcessQueryModel model)
        {
            return await service.LoggerProcess(model);
        }
        [HttpPost("datadevices")]
        public async Task<BaseResModel<LogDeviceStatusEntity>> LogDevices([FromBody] LogDeviceDataQueryModel model)
        {
            return await service.GetDataLogDeviceOnOff(model);
        }
    }
}
