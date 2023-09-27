using Common.Enum;

namespace Models
{
    public class QueryBaseModel
    {
        public string? SearchTerm { get; set; }
        public TypeOrderBy? typeOrderBy { get; set; }
        public string? Token { get; set; }

        public string? UserId { get; set; }
    }
}
