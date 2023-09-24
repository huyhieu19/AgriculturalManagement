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

        public async Task<bool> DeleteImage(int Id)
        {
            try
            {
                await repositoryManager.Image.DeleteImage(Id);
                await repositoryManager.SaveAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new ArgumentNullException(ex.Message);
            }
        }

        public async Task<IEnumerable<ImageDisplayModel>> GetImages(ImageQueryDisplayModel model)
        {
            try
            {
                var imagesModel = await repositoryManager.Image.GetImages(model);
                return mapper.Map<IEnumerable<ImageDisplayModel>>(imagesModel);
            }
            catch (Exception ex)
            {
                throw new ArgumentNullException(ex.Message);
            }
        }

        public async Task<bool> SetImage(ImageCreateModel model)
        {
            try
            {
                string Url = UploadImage.UploadImageRoot(model.FileImage);
                return await repositoryManager.Image.SetImage(model, Url);
            }
            catch (Exception ex)
            {
                throw new ArgumentNullException(ex.Message);
            }
        }

        public async Task SetImageDefault(int Id)
        {
            try
            {
                await repositoryManager.Image.SetImageDefault(Id);
            }
            catch (Exception ex)
            {
                throw new ArgumentNullException(ex.Message);
            }
        }
    }
}
