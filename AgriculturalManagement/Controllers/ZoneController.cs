using Microsoft.AspNetCore.Mvc;
using Models.Zone;
using Service.Contracts;

namespace AgriculturalManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZoneController : ControllerBase
    {
        private readonly IServiceManager serviceManager;
        public ZoneController(IServiceManager serviceManager) => this.serviceManager = serviceManager;

        [HttpPost, Route("zones")]
        public async Task<IEnumerable<ZoneDisplayModel>> GetZones([FromBody] ZoneQueryDisplayModel model) => await serviceManager.ZoneService.GetZones(model, trackChanges: false);
        [HttpPost, Route("zone")]
        public async Task<bool> AddZone([FromBody] ZoneCreateModel createModel) => await serviceManager.ZoneService.AddZone(createModel);
        [HttpDelete, Route("zone")]
        public async Task<bool> RemoveZone([FromQuery] int id) => await serviceManager.ZoneService.RemoveZone(id);
        [HttpPut, Route("zone")]
        public async Task<bool> UpdateZone([FromBody] ZoneUpdateModel updateModel) => await serviceManager.ZoneService.UpdateZone(updateModel);

    }
}
