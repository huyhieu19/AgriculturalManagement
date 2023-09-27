using Microsoft.AspNetCore.Mvc;
using Models;
using Service.Contracts;

namespace AgriculturalManagement.Controllers
{
    [Route("api/Zone")]
    [ApiController]
    public class ZoneController : ControllerBase
    {
        private readonly IServiceManager serviceManager;
        public ZoneController(IServiceManager serviceManager) => this.serviceManager = serviceManager;

        [HttpPost, Route("zones")]
        public async Task<IEnumerable<ZoneDisplayModel>> GetZones([FromBody] ZoneQueryDisplayModel model) => await serviceManager.Zone.GetZones(model, trackChanges: false);
        [HttpPost, Route("zone")]
        public async Task<bool> AddZone([FromBody] ZoneCreateModel createModel) => await serviceManager.Zone.AddZone(createModel);
        [HttpDelete, Route("zone")]
        public async Task<bool> RemoveZone([FromQuery] int id) => await serviceManager.Zone.RemoveZone(id);
        [HttpPut, Route("zone")]
        public async Task<bool> UpdateZone([FromBody] ZoneUpdateModel updateModel) => await serviceManager.Zone.UpdateZone(updateModel);

    }
}
