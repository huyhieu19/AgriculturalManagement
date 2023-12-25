using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.InstrumentSetThreshold;
using Service.Contracts;

namespace AgriculturalManagement.Controllers.Device
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstrumentSetThresholdController : ControllerBase
    {
        private readonly IServiceManager serviceManager;
        private readonly IHttpContextAccessor _contextAccessor;

        public InstrumentSetThresholdController(IServiceManager serviceManager, IHttpContextAccessor _contextAccessor)
        {
            this.serviceManager = serviceManager;
            this._contextAccessor = _contextAccessor;
        }
        [HttpGet, Route("Delete")]
        [Authorize(Roles = "Administrator")]
        public async Task<IEnumerable<InstrumentSetThresholdDisplayModel>> DeviceInstrumentOnOffDelete()
        {
            return await serviceManager.InstrumentSetThreshold.DeviceInstrumentOnOffDelete();
        }
        [HttpGet, Route("All")]
        [Authorize(Roles = "Administrator")]
        public async Task<IEnumerable<InstrumentSetThresholdDisplayModel>> DeviceInstrumentOnOff()
        {
            var userId = _contextAccessor.HttpContext!.User.FindFirst("Id")!.Value;
            return await serviceManager.InstrumentSetThreshold.DeviceInstrumentOnOff(userId);
        }
        [HttpGet, Route("DatatableByIdDeviceDrive")]
        [Authorize(Roles = "Administrator")]
        public async Task<IEnumerable<InstrumentSetThresholdDisplayModel>> DeviceInstrumentOnOffByIdDeviceDriver(Guid Id)
        {
            return await serviceManager.InstrumentSetThreshold.DeviceInstrumentOnOffByIdDeviceDriver(Id);
        }
        [HttpPost, Route("Update")]
        [Authorize(Roles = "Administrator")]
        public async Task<bool> DeviceInstrumentOnOffUpdate(InstrumentSetThresholdUpdateModel updateModel)
        {
            return await serviceManager.InstrumentSetThreshold.DeviceInstrumentOnOffUpdate(updateModel);
        }
        [HttpPost, Route("Create")]
        [Authorize(Roles = "Administrator")]
        public async Task<bool> DeviceInstrumentOnOffCreate(InstrumentSetThresholdCreateModel model)
        {
            return await serviceManager.InstrumentSetThreshold.DeviceInstrumentOnOffCreate(model);
        }
        [HttpPost, Route("DeleteById")]
        [Authorize(Roles = "Administrator")]
        public async Task<bool> DeviceInstrumentOnOffDeleteById(ThresholdRemoveModel model)
        {
            return await serviceManager.InstrumentSetThreshold.DeviceInstrumentOnOffDeleteById(model);
        }
    }
}