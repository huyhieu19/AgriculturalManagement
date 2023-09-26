using Models;

namespace Service.Contracts
{
    public interface IValueTypeService
    {
        Task<List<TypeTreeDisplayModel>> GetTypeTrees();
        Task<List<DeviceDriversTypeDisplayModel>> GetTypeDeviceDrivers();
        Task<List<InstrumentationTypeDisplayModel>> GetTypeInstrumentation();

        Task<bool> DeleteTypeTrees(TypeTreeDisplayModel model);
        Task<bool> DeleteTypeDeviceDrivers(DeviceDriversTypeDisplayModel model);
        Task<bool> DeleteTypeInstrumentations(InstrumentationTypeDisplayModel model);

        Task<bool> UpdateTypeTree(TypeTreeDisplayModel model);
        Task<bool> UpdateTypeDeviceDriver(DeviceDriversTypeDisplayModel model);
        Task<bool> UpdateTypeInstrumentation(InstrumentationTypeDisplayModel model);

        Task<bool> CreateTypeTrees(TypeTreeCreateModel model);
        Task<bool> CreateTypeDeviceDrivers(DeviceDriversTypeCreateModel model);
        Task<bool> CreateTypeInstrumentations(InstrumentationTypeCreateModel model);

    }
}
