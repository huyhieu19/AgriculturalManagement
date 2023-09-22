namespace Repository.Contracts
{
    public interface IRepositoryManager
    {
        IFarmRepository FarmRepository { get; }
        Task SaveAsync();
    }
}
