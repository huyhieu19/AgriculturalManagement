using Models;

namespace Service.Contracts
{
    public interface IImageService
    {
        Task<IEnumerable<ImageDisplayModel>> GetImages(ImageQueryDisplayModel model);
        Task<bool> SetImage(ImageCreateModel model);
        Task<bool> DeleteImage(int Id);
        Task SetImageDefault(int Id);
    }
}