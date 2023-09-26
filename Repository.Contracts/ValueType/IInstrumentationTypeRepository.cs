using Entities;

namespace Repository.Contracts
{
    public interface IInstrumentationTypeRepository
    {
        void CreateTypeInstrumentations(InstrumentationTypeEntity entity);
        void UpdateTypeInstrumentation(InstrumentationTypeEntity entity);
        void DeleteTypeInstrumentations(InstrumentationTypeEntity entity);
        Task<List<InstrumentationTypeEntity>> GetTypeInstrumentation();
    }
}
