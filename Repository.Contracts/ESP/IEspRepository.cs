using Entities.ESP;

namespace Repository.Contracts
{
    public interface IEspRepository
    {
        Task<List<EspEntity>> GetAll();
        void CreateEsp(EspEntity entity);
    }
}