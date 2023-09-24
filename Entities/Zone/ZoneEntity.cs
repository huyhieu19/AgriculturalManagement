﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    public class ZoneEntity
    {
        [Key]
        public int Id { get; set; }
        public string ZoneName { get; set; } = null!;
        public string? Description { get; set; }
        public string? Note { get; set; }

        public DateTime? TimeToStartPlanting { get; set; } = DateTime.Now;
        public string? Function { get; set; }
        public DateTime? CreateDate { get; set; } = DateTime.Now;

        [ForeignKey("Farm")]
        public int? FarmId { get; set; }
        [ForeignKey("Type")]
        public int? TypeTreeId { get; set; }

        public FarmEntity? Farm { get; set; }
        public TypeTreeEntity? Type { get; set; }

        public ICollection<ImageEntity>? Images { get; set; }
        public ICollection<ZoneDeviceDriverEntity>? ZoneDeviceDrivers { get; set; }
        public ICollection<InstrumentationEntity>? Instrumentations { get; set; }
        public List<ZoneHarvestEntity>? Harvests { get; set; }
    }
}