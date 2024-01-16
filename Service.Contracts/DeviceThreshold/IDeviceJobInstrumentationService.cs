namespace Service.Contracts.DeviceThreshold
{
    public interface IDeviceJobInstrumentationService
    {
        Task<bool> RunningJobThreshold();
    }
}
