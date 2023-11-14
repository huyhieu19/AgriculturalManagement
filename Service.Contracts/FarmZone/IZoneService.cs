using Models;
using Models.Zone;

namespace Service.Contracts.FarmZone
{
    public interface IZoneService
    {
        Task<List<ZoneDisplayModel>> GetZones(int farmId, bool trackChanges);
        Task<ZoneModifyResponseModel> AddZone(ZoneCreateModel createModel);
        Task<ZoneModifyResponseModel> RemoveZone(int id, int farmId);
        Task<ZoneModifyResponseModel> UpdateZone(ZoneUpdateModel updateModel);
    }
}