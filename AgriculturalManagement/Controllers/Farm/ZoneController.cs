using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Zone;
using Service.Contracts;

namespace AgriculturalManagement.Controllers.Farm
{
    [Route("api/Zone")]
    [ApiController]
    public class ZoneController : ControllerBase
    {
        private readonly IServiceManager serviceManager;
        public ZoneController(IServiceManager serviceManager) => this.serviceManager = serviceManager;

        [HttpPost, Route("zones")]
        [Authorize(Roles = "Administrator")]
        public async Task<List<ZoneDisplayModel>> GetZones([FromBody] ZoneQueryDisplayModel model) => await serviceManager.Zone.GetZones(model.FarmId, trackChanges: false);

        [HttpPost, Route("zone")]
        [Authorize(Roles = "Administrator")]
        public async Task<ZoneModifyResponseModel> AddZone([FromBody] ZoneCreateModel createModel) => await serviceManager.Zone.AddZone(createModel);

        [HttpDelete, Route("zone")]
        [Authorize(Roles = "Administrator")]
        public async Task<ZoneModifyResponseModel> RemoveZone(int id, int farmId) => await serviceManager.Zone.RemoveZone(id, farmId);

        [HttpPut, Route("zone")]
        [Authorize(Roles = "Administrator")]
        public async Task<ZoneModifyResponseModel> UpdateZone([FromBody] ZoneUpdateModel updateModel) => await serviceManager.Zone.UpdateZone(updateModel);

    }
}