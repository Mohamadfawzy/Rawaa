namespace Rawaa_Api.Helper
{
    public class ErrorClass
    {
        public ErrorClass(string StatusCode, string Message)
        {
            this.Message = Message;
            this.StatusCode  = StatusCode;
        }
        public string StatusCode { get; set; }
        public string Message { get; set; }
    }
}
