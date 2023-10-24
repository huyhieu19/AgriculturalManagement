using Models.ESP;

namespace Service.Contracts.ESP
{
    public interface IEspBackgroundProcessService
    {
        public Task<List<EspAndTopicModel>> GetAll();

    }
}
