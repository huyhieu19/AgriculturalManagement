﻿namespace Entities
{
    public class InstrumentationTypeEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Unit { get; set; }
        public string? Manufacturer { get; set; }
        public string? ImageUrl { get; set; }
        public ICollection<InstrumentationEntity>? Instrumentations { get; set; }

    }
}