using Models;

namespace Service.Contracts
{
    public interface IImageService
    {
        Task<bool> SetImgaeFarm(ImageCreateModel model);
        Task<bool> SetImgaeZone(ImageCreateModel model);
        Task<bool> SetImgaeInstrument(ImageCreateModel model);
        Task<bool> SetImgaeUser(ImageCreateModel model);
        Task<bool> SetImgaeMachine(ImageCreateModel model);
        Task<bool> SetImgaeDeviceDriver(ImageCreateModel model);

    }
}
