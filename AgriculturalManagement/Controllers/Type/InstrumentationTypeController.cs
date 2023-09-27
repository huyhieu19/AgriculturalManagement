using Microsoft.AspNetCore.Mvc;
using Models;
using Service.Contracts;

namespace AgriculturalManagement.Controllers.Type
{
    [Route("api/InstrumentationType")]
    [ApiController]
    public class InstrumentationTypeController : ControllerBase
    {
        private readonly IServiceManager service;

        public InstrumentationTypeController(IServiceManager service)
        {
            this.service = service;
        }
        [HttpGet, Route("InstrumentationTypes")]
        public async Task<List<InstrumentationTypeDisplayModel>> Get()
        {
            return await service.ValueType.GetTypeInstrumentation();
        }
        [HttpPost, Route("InstrumentationType")]
        public async Task<bool> Create(InstrumentationTypeCreateModel model)
        {
            return await service.ValueType.CreateTypeInstrumentations(model);
        }
        [HttpPut, Route("InstrumentationType")]
        public async Task<bool> Update(InstrumentationTypeDisplayModel model)
        {
            return await service.ValueType.UpdateTypeInstrumentation(model);
        }
        [HttpDelete, Route("InstrumentationType")]
        public async Task<bool> Delete(InstrumentationTypeDisplayModel model)
        {
            return await service.ValueType.DeleteTypeInstrumentations(model);
        }
    }
}
