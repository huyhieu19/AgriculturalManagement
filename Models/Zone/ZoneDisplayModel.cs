namespace Models
{
    public class ZoneDisplayModel
    {
        public int Id { get; set; }
        public string ZoneName { get; set; } = null!;
        public double? Area { get; set; }
        public string? Description { get; set; }
        public string? Note { get; set; }
        public DateTime? TimeToStartPlanting { get; set; }
        public DateTime? DateCreateFarm { get; set; }
        public string? Function { get; set; }
        public int? TypeTreeId { get; set; }
        public int? FarmId { get; set; }

        // adding

        public int? CountDeviceDriver { get; set; }
        public int? CountInstrumentation { get; set; }
    }
}