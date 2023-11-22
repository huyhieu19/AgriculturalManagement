namespace Models
{
    public class BaseResModel<T>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 50;
        public int TotalPage { get; set; } = 0;
        public int TotalCount { get; set; } = 0;
        public List<T>? Data { get; set; }
    }
}
