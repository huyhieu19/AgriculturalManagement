using Common.Enum;

namespace Models.ESP;

public class DeviceESPCreateModel
{
    public Guid EspId { get; set; }
    public string? Name { get; set; }
    public string? Gpio { get; set; }
    public bool IsAction { get; set; }
    public SensorType? DeviceType { get; set; }
    public Guid Topic { get; set; }
    public ResponseSensorType? ResponseType { get; set; }
}