using Entities;

namespace Repository.Contracts
{
    public interface IEspRepository
    {
        Task<List<Esp8266Entity>> GetAll();
        void CreateEsp(Esp8266Entity entity);
    }
}
