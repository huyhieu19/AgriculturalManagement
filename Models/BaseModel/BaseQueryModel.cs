namespace Models
{
    public class BaseQueryModel
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 50;
    }
}
