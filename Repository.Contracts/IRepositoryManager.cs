namespace Repository.Contracts
{
    public interface IRepositoryManager
    {
        IFarmRepository Fram { get; }
        Task SaveAsync();
    }
}
