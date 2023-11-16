using Models;

namespace Service.Contracts
{
    public interface IValueTypeService
    {
        Task<List<InstrumentationTypeDisplayModel>> GetTypeInstrumentation();

        Task<bool> DeleteTypeInstrumentations(InstrumentationTypeDisplayModel model);

        Task<bool> UpdateTypeInstrumentation(InstrumentationTypeDisplayModel model);

        Task<bool> CreateTypeInstrumentations(InstrumentationTypeCreateModel model);

    }
}