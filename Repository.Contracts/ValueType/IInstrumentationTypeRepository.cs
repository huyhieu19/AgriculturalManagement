using Entities;
using Models;

namespace Repository.Contracts
{
    public interface IInstrumentationTypeRepository
    {
        Task<bool> CreateTypeInstrumentations(List<InstrumentationTypeDisplayModel> model);
        Task<bool> UpdateTypeInstrumentation(InstrumentationTypeDisplayModel model);
        Task<bool> DeleteTypeInstrumentations(List<int> Ids);
        Task<List<InstrumentationTypeEntity>> GetTypeInstrumentation();
    }
}
