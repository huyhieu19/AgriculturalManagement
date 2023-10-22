namespace Models
{
    public class InstrumentationDisplayModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Note { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; } = false;
        public DateTime? DateStartedUsing { get; set; }
        public int? ZoneId { get; set; }
        public int? InstrumentationTypeId { get; set; }

        public Guid? EspId { get; set; }
        public string? Gpio { get; set; }

        // adding
        ///Farm
        public string? FarmName { get; set; }

        /// zone
        public string? ZoneName { get; set; }
        public string? ZoneDescription { get; set; }

        /// InstrumentationType
        public string? InstrumentationTypeName { get; set; }
        public string? InstrumentationTypeUnit { get; set; }
        public string? InstrumentationTypeManufacturer { get; set; }
    }
}
