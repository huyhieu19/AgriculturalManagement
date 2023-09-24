namespace Service.Contracts
{
    public interface IServiceManager
    {
        IFarmService FarmService { get; }
        IZoneService ZoneService { get; }
        IImageService Image { get; }
    }
}
