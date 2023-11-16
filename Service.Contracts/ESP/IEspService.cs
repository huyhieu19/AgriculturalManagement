using Models;
using Models.Device;

namespace Service.Contracts
{
    public interface IEspService
    {

        Task<List<EspDisplayModel>> GetEspsAll();
        Task<List<EspDisplayModel>> GetEsps(string id);
        Task<bool> CreateEsp(EspCreateModel model);
        Task<bool> DeleteESP(Guid id);

        Task<bool> AddEspToUser(Guid espId, string userId);

        Task<List<DeviceDisplayModel>> DeviceESPDisplay(Guid id);
        Task<bool> DeviceESPCreate(DeviceCreateModel model);
        Task<bool> DeviceESPRemove(Guid id);

    }
}