namespace Models
{
    public class StatusDeviceModel
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public bool IsProblem { get; set; }
        public string? Name { get; set; }
    }
}