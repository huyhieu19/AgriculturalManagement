using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Device;
using Models.Zone;
using Service.Contracts;

namespace AgriculturalManagement.Controllers.Farm
{
    [Route("api/Zone")]
    [ApiController]
    public class ZoneController : ControllerBase
    {
        private readonly IServiceManager serviceManager;
        public ZoneController(IServiceManager serviceManager)
        {
            this.serviceManager = serviceManager;
        }


        #region Information Zone
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
        #endregion

        #region Device On Zone
        [HttpPost("device-control-used")]
        public async Task<List<DeviceDisplayModel>> GetDevicesControlOnZone(int zoneId)
        {
            var response = await serviceManager.Device.GetDevicesControlOnZone(zoneId);
            return response;
        }
        [HttpPost("device-instrumentation-used")]
        public async Task<List<DeviceDisplayModel>> GetDevicesInstrumentationOnZone(int zoneId)
        {
            var response = await serviceManager.Device.GetDevicesInstrumentationOnZone(zoneId);
            return response;
        }
        [HttpPost("add-used-device")]
        public async Task<bool> DeviceAdd(Guid deviceId, int zoneId)
        {
            var response = await serviceManager.Device.AddDeviceToZone(deviceId, zoneId);
            return response;
        }
        [HttpPost("remove-used-device")]
        public async Task<bool> DeviceRemove(Guid deviceId, int zoneId)
        {
            var response = await serviceManager.Device.RemoveDeviceFromZone(deviceId, zoneId);
            return response;
        }
        [HttpPost, Route("set-auto-device")]
        public async Task<bool> SetAutoDevice(Guid deviceId, bool IsAuto)
        {
            var response = await serviceManager.Device.SetAutoDevice(deviceId, IsAuto);
            return response;
        }
        #endregion
    }
}