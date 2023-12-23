namespace Service.Contracts.DeviceThreshold
{
    public interface IDeviceJobInstrumentationService
    {
        Task<bool> RunningJobThreshold(Guid deviceId, string valueString, string typeDevice);
    }
}
