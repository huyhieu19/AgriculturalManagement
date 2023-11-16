using Entities.ESP;

namespace Repository.Contracts
{
    public interface IEspRepository
    {
        Task<List<EspEntity>> GetEsps(string id);
        void CreateEsp(EspEntity entity);
        void DeleteEsp(Guid id);
        Task<bool> AddEspToUser(Guid espId, string userId);
        Task<List<EspEntity>> GetEspsAll();
    }
}