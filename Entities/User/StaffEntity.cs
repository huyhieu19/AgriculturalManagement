using System.ComponentModel.DataAnnotations;

namespace Entities.User
{
    public class StaffEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Address { get; set; }
        public DateTime? Dob { get; set; }
        public DateTime? DateJoin { get; set; }
        public string? Position { get; set; }
        public int? OffWorksOfMonth { get; set; }
        public int? WorksOfMonth { get; set; }
        public int? NoteOfSalary { get; set; }
        public int? Salary { get; set; }
        public int? Bonus { get; set; }
        public ICollection<ImageEntity>? Images { get; set; }
    }
}
