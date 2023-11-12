using Models.ESP;

namespace Service.Contracts.Module
{
    public interface IEspBackgroundProcessService
    {
        public Task<List<EspAndTopicModel>> GetAll();
    }
}