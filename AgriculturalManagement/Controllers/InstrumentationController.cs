using Microsoft.AspNetCore.Mvc;
using Models;
using Service.Contracts;

namespace AgriculturalManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstrumentationController : ControllerBase
    {
        private readonly IServiceManager serviceManager;
        public InstrumentationController(IServiceManager serviceManager) => this.serviceManager = serviceManager;
        [HttpGet, Route("InstrumentationsByZone")]
        public async Task<IEnumerable<InstrumentationDisplayModel>> GetInstrumentationByZoneAsync(int Id)
        {
            return await serviceManager.InstrumentationService.GetInstrumentationByZoneAsync(Id);
        }
        [HttpGet, Route("InstrumentationsNotInZone")]
        public async Task<IEnumerable<InstrumentationDisplayModel>> GetInstrumentationNotInZoneAsync()
        {
            return await serviceManager.InstrumentationService.GetInstrumentationNotInZoneAsync();
        }
        [HttpPut, Route("Instrumentation")]
        public async Task<IActionResult> UpdateInforInstrumentation([FromBody] InstrumentationUpdateModel updateModel)
        {
            await serviceManager.InstrumentationService.UpdateInforInstrumentation(updateModel);
            return Ok(true);
        }
        [HttpDelete, Route("Instrumentation")]
        public async Task<IActionResult> DeleteInstrumentation(int Id)
        {
            await serviceManager.InstrumentationService.DeleteInstrumentation(Id);
            return Ok(true);

        } //  Xóa hẳn => bị hỏng máy
        [HttpPut, Route("InstrumentationRemove")] //chuyển Zone Id  = null
        public async Task<IActionResult> RemoveInstrumentation(int Id)
        {
            await serviceManager.InstrumentationService.RemoveInstrumentation(Id);
            return Ok(true);
        } //chuyển Zone Id  = null
        [HttpPost, Route("Instrumentation")]
        public async Task<IActionResult> CreateInstrumentation(InstrumentationCreateModel createModel)
        {
            await serviceManager.InstrumentationService.CreateInstrumentation(createModel);
            return Ok(true);
        }
    }
}
