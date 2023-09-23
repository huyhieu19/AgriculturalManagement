using Models;

namespace Service.Contracts
{
    public interface IInstrumentationService
    {
        Task<IEnumerable<InstrumentationDisplayModel>> GetInstrumentationDisplayModelAsync();
        Task<InstrumentationUpdateModel> UpdateInforInstrumentation();
        Task DeleteInstrumentation(int Id);
        Task AddInstrumentation(InstrumentationCreateModel model);
        Task<StatusDeviceModel> TurnOnTurnOff(int Id);
        Task<StatusDeviceModel> AsyncStatusMachine(int Id);

    }
}
