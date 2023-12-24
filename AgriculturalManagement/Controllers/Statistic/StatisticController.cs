using Microsoft.AspNetCore.Mvc;
using Models.Statistic;
using Service;

namespace AgriculturalManagement.Controllers.Statistic
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticController : ControllerBase
    {
        private readonly IDataStatisticsService service;

        public StatisticController(IDataStatisticsService service)
        {
            this.service = service;
        }

        [HttpGet("devicesByHour")]
        public async Task<List<StatisticDisplayModel>> StatisticsByDateDataDevices(Guid DeviceId)
        {
            return await service.StatisticsByDateDataDevices(DeviceId);
        }
    }
}
