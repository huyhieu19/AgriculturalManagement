using Entities;
using Models;

namespace Repository.Contracts
{
    public interface IInstrumentationRepository
    {
        Task<IEnumerable<InstrumentationDisplayModel>> GetInstrumentationByZoneAsync(int Id);
        Task<IEnumerable<InstrumentationEntity>> GetInstrumentationNotInZoneAsync();
        Task UpdateInforInstrumentation(InstrumentationUpdateModel updateModel);
        void DeleteInstrumentation(InstrumentationEntity entity); //  Xóa hẳn => bị hỏng máy
        void CreateInstrumentation(InstrumentationEntity createModel);
        Task RemoveInstrumentation(int Id); //chuyển Zone Id  = null
    }
}
