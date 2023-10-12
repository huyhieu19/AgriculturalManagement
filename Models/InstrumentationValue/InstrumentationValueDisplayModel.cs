namespace Models.InstrumentationValue
{
    public class InstrumentationValueDisplayModel
    {
        public string? Id { get; set; }
        public int InstrumentationId { get; set; }
        public string? Value { get; set; }
        public DateTime? ValueDate { get; set; }
    }
}
