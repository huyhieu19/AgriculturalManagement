namespace Models
{
    public class ResponseModel<T>
    {
        public bool Success { get; set; }
        public string? ErrorMessage { get; set; }
        public T? Data { get; set; }
        public int StatusCode { get; set; }

    }
}