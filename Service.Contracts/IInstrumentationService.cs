using Models;

namespace Service.Contracts
{
    public interface IInstrumentationService
    {
        Task<IEnumerable<InstrumentationDisplayModel>> GetInstrumentationByZoneAsync();
        Task<IEnumerable<InstrumentationDisplayModel>> GetInstrumentationNotInZoneAsync();
        Task UpdateInforInstrumentation(InstrumentationUpdateModel updateModel);
        Task DeleteInstrumentation(int Id); //  Xóa hẳn => bị hỏng máy
        Task RemoveInstrumentation(int Id); //chuyển Zone Id  = null

        Task<StatusDeviceModel> AsyncStatusInstrumentation(int Id);

    }
}
