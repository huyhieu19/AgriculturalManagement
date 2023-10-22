using Models;

namespace Service.Contracts
{
    public interface IEspService
    {
        Task<List<EspDisplayModel>> GetAll();
        Task CreateEsp(EspCreateModel model);
    }
}
