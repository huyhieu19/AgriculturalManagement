using Models;
using Service.Contracts;

namespace Service
{
    public class InstrumentationService : IInstrumentationService
    {
        public Task AddInstrumentation(InstrumentationCreateModel model)
        {
            throw new NotImplementedException();
        }

        public Task<StatusDeviceModel> AsyncStatusMachine(int Id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteInstrumentation(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<InstrumentationDisplayModel>> GetInstrumentationDisplayModelAsync()
        {
            throw new NotImplementedException();
        }

        public Task<StatusDeviceModel> TurnOnTurnOff(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<InstrumentationUpdateModel> UpdateInforInstrumentation()
        {
            throw new NotImplementedException();
        }
    }
}
