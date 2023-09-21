namespace Service.Contracts
{
    public interface IFarmService
    {
        Task<IEnumerable<IFarmService>> GetAllFarmAsync(bool trackChange);
    }
}
