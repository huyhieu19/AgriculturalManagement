namespace Models.Device
{
    public class DeviceModifyResponseModel
    {
        public List<DeviceDisplayModel> displayModels { get; set; }
        public bool? success { get; set; }
    }
}