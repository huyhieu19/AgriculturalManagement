using Models;

namespace Service.Contracts
{
    public interface IEspService
    {
        Task<List<EspDisplayModel>> GetAll();
        Task<bool> CreateEsp(EspCreateModel model);
    }
}