﻿using Microsoft.AspNetCore.Mvc;
using Models;
using Service.Contracts;

namespace AgriculturalManagement.Controllers
{
    [Route("api/[controller]")]
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
    }
}