namespace Repository.Contracts
{
    public interface IRepositoryManager
    {
        IFarmRepository FarmRepository { get; }
        IZoneRepository ZoneRepository { get; }
        IImageRepository ImageRepositoty { get; }
        Task SaveAsync();
    }
}
