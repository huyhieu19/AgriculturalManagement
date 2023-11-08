using Entities.ESP;

namespace Repository.Contracts;

public interface IDeviceEspRepository
{
    Task<List<DeviceTypeOnEspEntity>> DeviceESPDisplay(Guid id);
    void DeviceESPCreate(DeviceTypeOnEspEntity model);
    void DeviceESPRemove(Guid id);
}