namespace Models
{
    public class ApiResponseModel<T>
    {
        public bool Success { get; set; }
        public string? ErrorMessage { get; set; }
        public T? Data { get; set; }
        public int StatusCode { get; set; }

    }
}
