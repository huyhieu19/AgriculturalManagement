namespace Models
{
    public class ZoneHarvestDisplayModel
    {
        public int Id { get; set; }
        public DateTime HarvertTime { get; set; }
        public string? Note { get; set; }
        public int? ZoneId { get; set; }
    }
}
