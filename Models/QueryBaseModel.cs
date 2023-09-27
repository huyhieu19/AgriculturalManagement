using Common.Enum;

namespace Models
{
    public class QueryBaseModel
    {
        public string? SearchTerm { get; set; }
        public TypeOrderBy? typeOrderBy { get; set; }
    }
}
