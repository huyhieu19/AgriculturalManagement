using Entities;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.DeviceData;
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
        [HttpPost]
        public async Task<BaseResModel<InstrumentValueByFiveSecondEntity>> PullData([FromBody] DeviceDataQueryModel queryModel)
        {
            return await service.PullData(queryModel);
        }
    }
}
