using Common.Enum;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;

namespace AgriculturalManagement.Controllers.MockData
{
    [Route("api/[controller]")]
    [ApiController]
    public class MockDataController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public MockDataController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpPost("mock-data-module")]
        public async Task<ActionResult<bool>> MockDataModule(ModuleType Type)
        {
            return await _serviceManager.MockData.MockDataModule(Type);
        }
        [HttpPost("mock-data-device-on-module")]
        public async Task<bool> MockDataDeviceOnModule(Guid moduleId)
        {
            return await _serviceManager.MockData.MockDataDeviceOnModule(moduleId);
        }

        [HttpPost("delete-mock-devices-on-module")]
        public async Task<bool> DeleteMockDataDeviceOnModule(Guid moduleId)
        {
            return await _serviceManager.MockData.DeleteMockDataDeviceOnModule(moduleId);
        }
    }
}
