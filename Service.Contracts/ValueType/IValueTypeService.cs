using Models;

namespace Service.Contracts
{
    public interface IValueTypeService
    {
        Task<List<TypeTreeDisplayModel>> GetTypeTrees();
        Task<List<InstrumentationTypeDisplayModel>> GetTypeInstrumentation();

        Task<bool> DeleteTypeTrees(TypeTreeDisplayModel model);
        Task<bool> DeleteTypeInstrumentations(InstrumentationTypeDisplayModel model);

        Task<bool> UpdateTypeTree(TypeTreeDisplayModel model);
        Task<bool> UpdateTypeInstrumentation(InstrumentationTypeDisplayModel model);

        Task<bool> CreateTypeTrees(TypeTreeCreateModel model);
        Task<bool> CreateTypeInstrumentations(InstrumentationTypeCreateModel model);

    }
}