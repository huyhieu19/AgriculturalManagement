namespace Models
{
    public class ZoneDisplayModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public ICollection<ImageDisplayModel>? Images { get; set; }
        public string? Description { get; set; }
        public string? Note { get; set; }
        public DateTime? HarvestTime { get; set; }
        public DateTime? TimeToStartPlanting { get; set; } = DateTime.Now;
        public string? Function { get; set; }
        public int? TypeTreeId { get; set; }
        public int? FarmId { get; set; }
    }
}
