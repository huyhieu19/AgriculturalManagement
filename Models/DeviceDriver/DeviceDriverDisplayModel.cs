﻿namespace Models
{
    public class DeviceDriverDisplayModel
    {
        public int Id { get; set; }
        public DateTime? DateStartedUsing { get; set; }
        public bool? IsAction { get; set; } = false;
        public bool? IsProblem { get; set; } = false;
        public int? ZoneId { get; set; }
        public int? DeviceDriverTypeId { get; set; }

        public Guid? EspId { get; set; }
        public string? Gpio { get; set; }

        public string? Topic
        {
            get
            {
                return $"{EspId}/D/{Id}";
            }
        }

        // Add
        public string? FarmName { get; set; }

        public string? ZoneName { get; set; }
        public string? ZoneDescription { get; set; }

        public string? DeviceDriverTypeName { get; set; }
        public string? DeviceDriverTypeManufacturer { get; set; }
        public string? DeviceDriverTypeImageUrl { get; set; }
    }
}
