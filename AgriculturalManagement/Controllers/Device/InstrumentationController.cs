using Microsoft.AspNetCore.Mvc;
using Service.Contracts;

namespace AgriculturalManagement.Controllers.Device
{
    [Route("api/Instrumentation")]
    [ApiController]
    public class InstrumentationController : ControllerBase
    {
        private readonly IServiceManager serviceManager;
        public InstrumentationController(IServiceManager serviceManager) => this.serviceManager = serviceManager;
        //[HttpGet, Route("InstrumentationsByZone")]
        //public async Task<IEnumerable<InstrumentationDisplayModel>> GetInstrumentationByZoneAsync(int Id)
        //{
        //    return await serviceManager.Instrumentation.GetInstrumentationByZoneAsync(Id);
        //}
        //[HttpGet, Route("InstrumentationsNotInZone")]
        //public async Task<IEnumerable<InstrumentationDisplayModel>> GetInstrumentationNotInZoneAsync()
        //{
        //    return await serviceManager.Instrumentation.GetInstrumentationNotInZoneAsync();
        //}
        //[HttpPut, Route("Instrumentation")]
        //public async Task<IActionResult> UpdateInforInstrumentation([FromBody] InstrumentationUpdateModel updateModel)
        //{
        //    await serviceManager.Instrumentation.UpdateInforInstrumentation(updateModel);
        //    return Ok(true);
        //}
        //[HttpDelete, Route("Instrumentation")]
        //public async Task<IActionResult> DeleteInstrumentation(int Id)
        //{
        //    await serviceManager.Instrumentation.DeleteInstrumentation(Id);
        //    return Ok(true);

        //} //  Xóa hẳn => bị hỏng máy
        //[HttpPut, Route("InstrumentationRemove")] //chuyển Zone Id  = null
        //public async Task<IActionResult> RemoveInstrumentation(int Id)
        //{
        //    await serviceManager.Instrumentation.RemoveInstrumentation(Id);
        //    return Ok(true);
        //}
        ////chuyển Zone Id  = null
        //[HttpPost, Route("Instrumentation")]
        //public async Task<IActionResult> CreateInstrumentation(InstrumentationCreateModel createModel)
        //{
        //    await serviceManager.Instrumentation.CreateInstrumentation(createModel);
        //    return Ok(true);
        //}
    }
}