using System.Net;

namespace Utils.HttpResponseModels
{
    public class HttpResponse<T>
    {
        public HttpStatusCode StatusCode { get; set; }

        public string Message { get; set; }

        public int? TotalRecords { get; set; }

        public T? Data { get; set; }

        public HttpResponse(
            HttpStatusCode statusCode,
            string message,
            int? totalRecords = null,
            T? data = default
        )
        {
            StatusCode = statusCode;
            Message = message;
            TotalRecords = totalRecords;
            Data = data;
        }
    }
}
