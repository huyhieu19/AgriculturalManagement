using Database;
using Entities.ESP;
using Microsoft.EntityFrameworkCore;
using Repository.Contracts.Device;

namespace Repository.Device
{
    public class DeviceRepository : RepositoryBase<DeviceEntity>, IDeviceRepository
    {
        public DeviceRepository(FactDbContext factDbContext) : base(factDbContext)
        {
        }

        public async Task DeviceCreate(Guid deviceId, int ZoneId)
        {
            // -Thêm Device bằng cách ,DeviceId->isUsed = true.
            var entity = await FactDbContext.DeviceEntities.FindAsync(deviceId);
            if (entity == null)
            {
                throw new ArgumentException("Device not found");
            }
            entity.IsUsed = true;
            entity.ZoneId = ZoneId;
            await Task.CompletedTask;
        }

        public async Task DeviceRemove(Guid deviceId, int ZoneId)
        {
            // - Xoá Device bằng cách Chon DeviceId -> isUsed = false.
            var entity = await FactDbContext.DeviceEntities.FindAsync(deviceId);
            if (entity == null)
            {
                throw new ArgumentException("Device not found");
            }
            entity.IsUsed = true;
            entity.ZoneId = null;
            await Task.CompletedTask;
        }

        // - Display Device -> Get device have ZoneId = param ZoneId, isUsed = true
        public async Task<List<DeviceEntity>> DevicesDisplay(int zoneId)
        {
            var entities = await FindByCondition(prop => prop.ZoneId == zoneId && prop.IsUsed == true, false).ToListAsync();
            return entities;
        }
    }
}
