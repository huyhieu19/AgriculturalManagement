using Entities;
using Models;

namespace Repository.Contracts
{
    public interface IInstrumentationRepository
    {
        Task<IEnumerable<InstrumentationEntity>> GetInstrumentationByZoneAsync(int Id);
        Task<IEnumerable<InstrumentationEntity>> GetInstrumentationNotInZoneAsync();
        Task UpdateInforInstrumentation(InstrumentationUpdateModel updateModel);
        Task DeleteInstrumentation(int Id); //  Xóa hẳn => bị hỏng máy
        void CreateInstrumentation(InstrumentationEntity createModel);
        Task RemoveInstrumentation(int Id); //chuyển Zone Id  = null
    }
}
