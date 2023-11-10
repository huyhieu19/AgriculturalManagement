using Entities.ESP;

namespace Repository.Contracts;

public interface IDeviceEspRepository
{
    Task<List<DeviceTypeEspEntity>> DeviceESPDisplay(Guid id);
    void DeviceESPCreate(DeviceTypeEspEntity entity);
    void DeviceESPRemove(Guid id);
}