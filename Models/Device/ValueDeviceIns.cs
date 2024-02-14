using Common.Enum;

namespace Models.Device
{
    public class ValueDeviceIns
    {
        public Guid? Id { get; set; }
        public Guid? ModuleId { get; set; }
        public int? ZoneId { get; set; }
        public string? Name { get; set; }
        public FunctionDeviceType? NameRef { get; set; }
        public string? Description { get; set; }
        public DateTime? DateCreated { get; set; }
        public bool? IsAction { get; set; }
        public bool? IsUsed { get; set; }
        public bool? IsAuto { get; set; }
        public string? Unit { get; set; }
        public string? Gate { get; set; }
        public string? DeviceType { get; set; }


        public string? ValueDevice { get; set; }
        public DateTime? DateValue { get; set; }
        public bool? IsErrored { get; set; } = false;
    }
}
