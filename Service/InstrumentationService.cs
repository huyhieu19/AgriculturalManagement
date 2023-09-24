using Models;
using Service.Contracts;

namespace Service
{
    public class InstrumentationService : IInstrumentationService
    {
        public Task<StatusDeviceModel> AsyncStatusInstrumentation(int Id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteInstrumentation(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<InstrumentationDisplayModel>> GetInstrumentationByZoneAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<InstrumentationDisplayModel>> GetInstrumentationNotInZoneAsync()
        {
            throw new NotImplementedException();
        }

        public Task RemoveInstrumentation(int Id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateInforInstrumentation(InstrumentationUpdateModel updateModel)
        {
            throw new NotImplementedException();
        }
    }
}
