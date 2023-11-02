using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class JobInZoneEntity
    {
        [Key]
        public int Id { get; set; }
        public string NameJob { get; set; } = null!;
        public DateTime? DateCreate { get; set; }
        public DateTime? ReminderDate { get; set; }
        public string? Note { get; set; }
        public string? ImageUrl { get; set; }
        public int? ZoneId { get; set; }
        public ZoneEntity? Zone { get; set; }
    }
}