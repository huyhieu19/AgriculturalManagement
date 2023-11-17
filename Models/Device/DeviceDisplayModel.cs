﻿namespace Models.Device
{
    public class DeviceDisplayModel
    {
        public Guid Id { get; set; }
        public Guid ModuleId { get; set; }
        public int? ZoneId { get; set; }
        public string? Name { get; set; }
        public string? NameRef { get; set; }
        public string? Description { get; set; }
        public DateTime? DateCreated { get; set; }
        public bool IsAction { get; set; }
        public bool IsUsed { get; set; }
        public bool IsAuto { get; set; }
        public string? Unit { get; set; }
        public string? Gate { get; set; }
        public string? DeviceType { get; set; }
    }
}