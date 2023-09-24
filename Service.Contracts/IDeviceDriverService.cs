using Models;

namespace Service.Contracts
{
    public interface IDeviceDriverService
    {
        Task<IEnumerable<InstrumentationDisplayModel>> GetDeviceDriverByZoneAsync(int Id);
        Task<IEnumerable<InstrumentationDisplayModel>> GetInstrumentationNotInZoneAsync();
        Task UpdateInforInstrumentation(InstrumentationUpdateModel updateModel);
        Task CreateInstrumentation(InstrumentationCreateModel createModel);
        Task DeleteInstrumentation(int Id); //  Xóa hẳn => bị hỏng máy
        Task RemoveInstrumentation(int Id); //chuyển Zone Id  = null
    }
}
