using Entities;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace AgriculturalManagement.Controllers.Device
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataDeviceController : ControllerBase
    {
        private readonly IDataStatisticsService service;

        public DataDeviceController(IDataStatisticsService service)
        {
            this.service = service;
        }
        [HttpGet]
        public async Task<List<InstrumentValueByFiveSecondEntity>> PullData()
        {
            return await service.PullData();
        }
    }
}
