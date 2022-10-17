namespace Rawaa_Api.Helper
{
    public class ErrorClass
    {
        public ErrorClass(string statusCode="000", string message = "There error")
        {
            this.Message = message;
            this.StatusCode  = statusCode;
            this.IsError = false;
        }
        public bool IsError { get; set; }
        public string StatusCode { get; set; }
        public string Message { get; set; }
    }
}
