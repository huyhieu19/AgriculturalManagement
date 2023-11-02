using Models;

namespace Service.Contracts
{
    public interface IInstrumentationService
    {
        Task<IEnumerable<InstrumentationDisplayModel>> GetInstrumentationByZoneAsync(int Id);
        Task<IEnumerable<InstrumentationDisplayModel>> GetInstrumentationNotInZoneAsync();
        Task UpdateInforInstrumentation(InstrumentationUpdateModel updateModel);
        Task CreateInstrumentation(InstrumentationCreateModel createModel);
        Task DeleteInstrumentation(int Id); //  Xóa hẳn => bị hỏng máy
        Task RemoveInstrumentation(int Id); //chuyển Zone Id  = null
        //Todo: create logic when creating background service
        Task<StatusDeviceModel> AsyncStatusInstrumentation(int Id);

    }
}