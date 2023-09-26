using Microsoft.AspNetCore.Mvc;
using Models;
using Service.Contracts;

namespace AgriculturalManagement.Controllers.Type
{
    [Route("api/[controller]")]
    [ApiController]
    public class TreeTypeController : ControllerBase
    {
        private readonly IServiceManager service;

        public TreeTypeController(IServiceManager service)
        {
            this.service = service;
        }

        [HttpGet, Route("typetrees")]
        public async Task<List<TypeTreeDisplayModel>> GetTypeTrees()
        {
            return await service.ValueType.GetTypeTrees();
        }
        [HttpPost, Route("typetree")]
        public async Task<bool> CreateTypeTrees(TypeTreeCreateModel model)
        {
            return await service.ValueType.CreateTypeTrees(model);
        }
        [HttpPut, Route("typetree")]
        public async Task<bool> UpdateTypeTree(TypeTreeDisplayModel model)
        {
            return await service.ValueType.UpdateTypeTree(model);
        }
        [HttpDelete, Route("typetree")]
        public async Task<bool> DeleteTypeTrees(TypeTreeDisplayModel model)
        {
            return await service.ValueType.DeleteTypeTrees(model);
        }
    }
}
