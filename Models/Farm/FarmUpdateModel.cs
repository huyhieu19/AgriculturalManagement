﻿namespace Models
{
    public class FarmUpdateModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public double? Area { get; set; }
        public string? Description { get; set; }
        public string? Address { get; set; }
        public string? Note { get; set; }
        public DateTime? DateCreated { get; set; } = DateTime.UtcNow;
        public string? UserId { get; set; }
    }
}