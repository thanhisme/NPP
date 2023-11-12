using System.Net;

namespace Utils.HttpResponseModels
{
    public class HttpExceptionResponse : Exception
    {
        public HttpStatusCode StatusCode { get; set; }
        public object? Errors { get; set; }

        public HttpExceptionResponse() { }

        public HttpExceptionResponse(
            HttpStatusCode statusCode,
            string message,
            object? errors = null
        ) : base(message)
        {
            StatusCode = statusCode;
            Errors = errors;
        }
    }
}
