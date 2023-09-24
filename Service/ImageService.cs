using AutoMapper;
using Common;
using Models;
using Repository.Contracts;
using Service.Contracts;

namespace Service
{
    public class ImageService : IImageService
    {
        private readonly IRepositoryManager repositoryManager;
        private readonly IMapper mapper;

        public ImageService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            this.repositoryManager = repositoryManager;
            this.mapper = mapper;
        }
        public async Task<IEnumerable<ImageDisplayModel>> GetImages(ImageQueryDisplayModel model)
        {
            var imagesModel = await repositoryManager.ImageRepositoty.GetImages(model);
            return mapper.Map<IEnumerable<ImageDisplayModel>>(imagesModel);
        }

        public async Task<bool> SetImage(ImageCreateModel model)
        {
            string Url = UploadImage.UploadImageRoot(model.FileImage);
            return await repositoryManager.ImageRepositoty.SetImage(model, Url);
        }
    }
}
