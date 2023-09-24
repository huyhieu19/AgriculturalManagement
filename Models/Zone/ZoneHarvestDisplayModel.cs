namespace Models
{
    public class ZoneHarvestDisplayModel
    {
        public int Id { get; set; }
        public DateTime HarvertTime { get; set; }
        public double? Quantity { get; set; } // default kg
        public string? Note { get; set; }
        public int? ZoneId { get; set; }
    }
}
