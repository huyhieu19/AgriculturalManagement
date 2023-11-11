﻿using Entities.CommonType;
using Entities.ESP;
using Entities.Farm;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    public class ZoneEntity
    {
        [Key]
        public int Id { get; set; }
        public string ZoneName { get; set; } = null!;
        public string? Description { get; set; }
        public double? Area { get; set; }
        public string? Note { get; set; }

        public DateTime? TimeToStartPlanting { get; set; } = DateTime.Now;
        public string? Function { get; set; }
        public DateTime? DateCreateFarm { get; set; } = DateTime.Now;

        [ForeignKey("Farms")]
        public int? FarmId { get; set; }
        [ForeignKey("TypeTree")]
        public int? TypeTreeId { get; set; }

        public FarmEntity? Farm { get; set; }
        public TypeTreeEntity? TypeTree { get; set; }
        public ICollection<DeviceEntity>? Devices { get; set; }
        public ICollection<ZoneHarvestEntity>? Harvests { get; set; }
        public ICollection<JobInZoneEntity>? JobInZones { get; set; }

    }
}