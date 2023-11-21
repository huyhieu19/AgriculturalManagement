using Entities;
using Microsoft.AspNetCore.Mvc;
using Models;
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

        [HttpPost]
        public Task<BaseResModel<LogProcessModel>> LogProcess([FromBody] LoggerProcessQueryModel model)
        {
            return service.LoggerProcess(model);
        }
    }
}
