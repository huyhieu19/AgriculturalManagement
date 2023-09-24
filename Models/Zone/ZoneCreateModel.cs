﻿namespace Models
{
    public class ZoneCreateModel
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public string? Note { get; set; }
        public DateTime? HarvestTime { get; set; }
        public DateTime? TimeToStartPlanting { get; set; } = DateTime.Now;
        public string? Function { get; set; }
        public int? TypeTreeId { get; set; }
        public int? FarmId { get; set; }
    }
}