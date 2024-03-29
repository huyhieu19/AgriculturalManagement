﻿namespace Models
{
    public class FarmDisplayModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime? DateCreated { get; set; }
        public string? Description { get; set; }
        public string? Address { get; set; }
        public double? Area { get; set; }
        public string? Note { get; set; }

        // Adding
        public int? CountZone { get; set; }
    }
}