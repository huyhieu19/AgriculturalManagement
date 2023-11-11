namespace Models.Device
{
    public class DeviceModifyResponseModel
    {
        public List<DeviceDisplayModel>? deviceDisplays { get; set; }
        public bool? isSuccess { get; set; }
    }
}