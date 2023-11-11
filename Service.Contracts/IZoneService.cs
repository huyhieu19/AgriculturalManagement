using Models;

namespace Service.Contracts
{
    public interface IZoneService
    {
        Task<IEnumerable<ZoneDisplayModel>> GetZones(int farmId, bool trackChanges);
        Task<bool> AddZone(ZoneCreateModel createModel);
        Task<bool> RemoveZone(int id);
        Task<bool> UpdateZone(ZoneUpdateModel updateModel);
    }
}