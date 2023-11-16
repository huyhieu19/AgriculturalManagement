using Entities.Image;
using Models;

namespace Repository.Contracts.Image
{
    public interface IImageRepository
    {
        Task<IEnumerable<ImageEntity>> GetImages(ImageQueryDisplayModel model);
        Task<bool> SetImage(ImageCreateModel model, string Url);
        Task<bool> DeleteImage(int Id);
        Task SetImageDefault(int Id);
    }
}