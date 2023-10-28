using Microsoft.AspNetCore.Mvc;
using Models;
using Service.Contracts;

namespace AgriculturalManagement.Controllers.Image
{
    [Route("api/Image")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IServiceManager serviceManager;
        public ImageController(IServiceManager serviceManager) => this.serviceManager = serviceManager;
        [HttpPost, Route("images")]
        public async Task<IEnumerable<ImageDisplayModel>> GetImages(ImageQueryDisplayModel model)
        {
            return await serviceManager.Image.GetImages(model);
        }
        [HttpPost, Route("image")]
        public async Task<bool> SetImage([FromForm] ImageCreateModel model)
        {
            return await serviceManager.Image.SetImage(model);
        }
        [HttpDelete, Route("image")]
        public async Task<bool> DeleteImage(int id)
        {
            return await serviceManager.Image.DeleteImage(id);
        }
        [HttpPut, Route("imagedefault")]
        public async Task<IActionResult> SetImageDefault(int id)
        {
            await serviceManager.Image.SetImageDefault(id);
            return Ok(true);
        }
    }
}
