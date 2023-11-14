using Entities;

namespace Repository.Contracts.Farm
{
    public interface IZoneRepository
    {
        void CreateZone(ZoneEntity entity);
        void DeleteZone(int id, int farmId);
        void UpdateZone(ZoneEntity entity);
        Task<IEnumerable<ZoneEntity>> GetZones(int farmId, bool trackChanges);
    }
}