namespace Managment.api.Models
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public T Data { get; set; }
        public string? Message { get; set; }

        public ApiResponse() { }

        public ApiResponse(bool sucess, T data, string? message = "")
        {
            Success = sucess;
            Data = data;
            Message = message;
        }
    }

}
