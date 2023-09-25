namespace Models
{
    public class ZoneUpdateModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? Note { get; set; }
        public DateTime? TimeToStartPlanting { get; set; }
        public DateTime? DateCreateFarm { get; set; }
        public string? Function { get; set; }
        public int? TypeTreeId { get; set; }
        public int? FarmId { get; set; }
    }
}
