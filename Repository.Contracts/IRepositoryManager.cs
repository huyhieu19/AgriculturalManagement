namespace Repository.Contracts
{
    public interface IRepositoryManager
    {
        IFarmRepository FarmRepository { get; }
        IZoneRepository ZoneRepository { get; }
        IImageRepository ImageRepositoty { get; }
        IInstrumentationRepository Instrumentation { get; }
        Task SaveAsync();
    }
}
