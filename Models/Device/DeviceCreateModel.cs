using Common.Enum;

namespace Models.Device;

public class DeviceCreateModel
{
    public Guid EspId { get; set; }
    public string? Name { get; set; }
    public GpioGateType? Gpio { get; set; }
    public bool IsUsed { get; set; }
    public DeviceType? DeviceType { get; set; }
    public Guid Topic { get; set; }
    public ResponseSensorType? ResponseType { get; set; }
}