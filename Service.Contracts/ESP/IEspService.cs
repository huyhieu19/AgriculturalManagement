using Models;
using Models.ESP;

namespace Service.Contracts
{
    public interface IEspService
    {

        Task<List<EspDisplayModel>> GetEspsAll();
        Task<List<EspDisplayModel>> GetEsps(string id);
        Task<bool> CreateEsp(EspCreateModel model);
        Task<bool> DeleteESP(Guid id);

        Task<bool> AddEspToUser(Guid espId, string userId);

        Task<List<DeviceESPDisplayModel>> DeviceESPDisplay(Guid id);
        Task<bool> DeviceESPCreate(DeviceESPCreateModel model);
        Task<bool> DeviceESPRemove(Guid id);

    }
}