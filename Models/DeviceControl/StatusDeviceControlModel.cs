namespace Models.DeviceControl
{
    public class StatusDeviceControlModel
    {
        public Guid ModuleId { get; set; }
        public Guid Id { get; set; }
        public bool IsAction { get; set; }
        public bool IsUsed { get; set; }
    }
}
