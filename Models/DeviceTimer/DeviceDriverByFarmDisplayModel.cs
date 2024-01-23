using Common.Enum;

namespace Models.DeviceTimer
{
    public class DeviceDriverByFarmDisplayModel
    {
        public List<KeyValueForFarm> Farms { get; set; } = new List<KeyValueForFarm>();
        public List<KeyValueForZone> Zone { get; set; } = new List<KeyValueForZone>();
        public List<KeyValueForDevice> Device { get; set; } = new List<KeyValueForDevice>();
    }

    public class KeyValueForFarm
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
    }
    public class KeyValueForZone
    {
        public int? Id { get; set; }
        public int? FarmId { get; set; }
        public string? Name { get; set; }
    }
    public class KeyValueForDevice
    {
        public string? Id { get; set; }
        public int? ZoneId { get; set; }
        public string? Name { get; set; }
        public FunctionDeviceType? NameRef { get; set; }
    }
}
