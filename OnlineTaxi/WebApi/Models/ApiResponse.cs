namespace WebApi.Models
{
    public class ApiResponse
    {
        public bool Success { get; set; }
        public int ResponseCode { get; set; }
        public dynamic Data { get; set; }
        public string ErrorMessage { get; set; }
    }
}
