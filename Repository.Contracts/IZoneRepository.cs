using Entities;
using Models;
using Models.Zone;

namespace Repository.Contracts
{
    public interface IZoneRepository
    {
        void CreateZone(ZoneEntity entity);
        void DeleteZone(int id);
        void UpdateZone(ZoneUpdateModel model);
        Task<IEnumerable<ZoneEntity>> GetZones(QueryBaseModel model, bool trackchanges);
    }
}
