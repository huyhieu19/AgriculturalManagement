using Common.Enum;

namespace Models.ESP;

public class DeviceESPCreateModel
{
    public Guid EspId { get; set; }
    public string? Name { get; set; }
    public GpioGateType? Gpio { get; set; }
    public bool IsUsed { get; set; }
    public SensorType? DeviceType { get; set; }
    public Guid Topic { get; set; }
    public ResponseSensorType? ResponseType { get; set; }
}