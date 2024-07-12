namespace Shopping.Web.Models.DTO
{
    public class ResponseDTO
    {
        public object? Result { get; set; } = null;
        public bool Success { get; set; } = false;
        public string Message { get; set; } = string.Empty;
    }
}
