using Microsoft.AspNetCore.Mvc;
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

        //[HttpPost("devicesByHour")]
        //public async Task<List<StatisticByDateDisplayModel>> StatisticsByDateDataDevices(StatisticQueryModel model)
        //{
        //    return await service.StatisticsByDateDataDevices(model);
        //}
    }
}
