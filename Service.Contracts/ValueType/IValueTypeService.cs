using Models;

namespace Service.Contracts
{
    public interface IValueTypeService
    {
        Task<List<TypeTreeDisplayModel>> GetTypeTrees();
        Task<List<DeviceDriversTypeDisplayModel>> GetTypeDeviceDrivers();
        Task<List<InstrumentationTypeDisplayModel>> GetTypeInstrumentation();

        Task<bool> DeleteTypeTrees(TypeTreeDisplayModel model);
        Task<bool> DeleteTypeDeviceDrivers(List<int> Ids);
        Task<bool> DeleteTypeInstrumentations(List<int> Ids);

        Task<bool> UpdateTypeTree(TypeTreeDisplayModel model);
        Task<bool> UpdateTypeDeviceDriver(DeviceDriversTypeDisplayModel model);
        Task<bool> UpdateTypeInstrumentation(InstrumentationTypeDisplayModel model);

        Task<bool> CreateTypeTrees(TypeTreeCreateModel model);
        Task<bool> CreateTypeDeviceDrivers(List<DeviceDriversTypeDisplayModel> model);
        Task<bool> CreateTypeInstrumentations(List<InstrumentationTypeDisplayModel> model);

    }
}
