using Entities;
using Models;

namespace Repository.Contracts
{
    public interface IImageRepository
    {
        Task<IEnumerable<ImageEntity>> GetImages(ImageQueryDisplayModel model);
        Task<bool> SetImage(ImageCreateModel model, string Url);
        Task<bool> DeleteImage(int Id);
    }
}
