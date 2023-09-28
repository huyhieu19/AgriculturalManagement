namespace Models
{
    public class DeviceDriverUpdateModel
    {
        public int Id { get; set; }
        public DateTime? DateStartedUsing { get; set; }
        public bool? IsAction { get; set; } = false;
        public bool? IsProblem { get; set; } = false;
        public int? ZoneId { get; set; }
        public int? DeviceDriverTypeId { get; set; }
    }
}
