using Entities.ESP;

namespace Repository.Contracts;

public interface IDeviceEspRepository
{
    Task<List<DeviceEntity>> DeviceESPDisplay(Guid id);
    void DeviceESPCreate(DeviceEntity entity);
    void DeviceESPRemove(Guid id);
}