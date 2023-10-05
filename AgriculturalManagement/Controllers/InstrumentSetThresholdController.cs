﻿using Microsoft.AspNetCore.Mvc;
using Models;
using Service.Contracts;

namespace AgriculturalManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstrumentSetThresholdController : ControllerBase
    {
        private readonly IServiceManager serviceManager;

        public InstrumentSetThresholdController(IServiceManager serviceManager)
        {
            this.serviceManager = serviceManager;
        }
        [HttpGet, Route("NotDelete")]
        public async Task<IEnumerable<InstrumentSetThresholdDisplayModel>> DeviceInstrumentOnOffNotDelete()
        {
            return await serviceManager.InstrumentSetThreshold.DeviceInstrumentOnOffNotDelete();
        }
        [HttpGet, Route("Delete")]
        public async Task<IEnumerable<InstrumentSetThresholdDisplayModel>> DeviceInstrumentOnOffDelete()
        {
            return await serviceManager.InstrumentSetThreshold.DeviceInstrumentOnOffDelete();
        }
        [HttpGet, Route("All")]
        public async Task<IEnumerable<InstrumentSetThresholdDisplayModel>> DeviceInstrumentOnOff()
        {
            return await serviceManager.InstrumentSetThreshold.DeviceInstrumentOnOff();
        }
        [HttpGet, Route("DatatableByIdDeviceDrive")]
        public async Task<IEnumerable<InstrumentSetThresholdDisplayModel>> DeviceInstrumentOnOffByIdDeviceDriver(int Id)
        {
            return await serviceManager.InstrumentSetThreshold.DeviceInstrumentOnOffByIdDeviceDriver(Id);
        }
        [HttpPost, Route("Update")]
        public async Task<IActionResult> DeviceInstrumentOnOffUpdate(InstrumentSetThresholdUpdateModel updateModel)
        {
            await serviceManager.InstrumentSetThreshold.DeviceInstrumentOnOffUpdate(updateModel);
            return Ok(true);
        }
        [HttpPost, Route("Create")]
        public async Task<IActionResult> DeviceInstrumentOnOffCreate(InstrumentSetThresholdCreateModel model)
        {
            await serviceManager.InstrumentSetThreshold.DeviceInstrumentOnOffCreate(model);
            return Ok(true);
        }
        [HttpDelete, Route("DeleteById")]
        public async Task<IActionResult> DeviceInstrumentOnOffDeleteById(int Id)
        {
            await serviceManager.InstrumentSetThreshold.DeviceInstrumentOnOffDeleteById(Id);
            return Ok(true);
        }
    }
}
