namespace CalculosMVC.Models
{
    public class ErrorViewModel
    {
        public int? StatusCode { get; }

        public string? Message { get; }
        public string? Path { get; }
        //public string? StackTrace { get; }
        public ErrorViewModel(int statusCode, string message, string path) 
        {
            StatusCode = statusCode;
            Message = message;
            Path = path;
            //StackTrace = stack;
        }
    }
}