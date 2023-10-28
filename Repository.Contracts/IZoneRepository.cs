using Entities;

namespace Repository.Contracts
{
    public interface IZoneRepository
    {
        void CreateZone(ZoneEntity entity);
        void DeleteZone(int id);
        void UpdateZone(ZoneEntity entity);
        Task<IEnumerable<ZoneEntity>> GetZones(int farmId, bool trackchanges);
    }
}
