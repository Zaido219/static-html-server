namespace StaticHtmlServer.Infrastructure
{
    public class HttpResponse
    {
        public int StatusCode { get; }
        public string ContentType { get; }
        public byte[] Body { get; }

        public HttpResponse(int statusCode, string contentType, byte[] body)
        {
            StatusCode = statusCode;
            ContentType = contentType;
            Body = body;

        }
        // Factory methods make creating responses clean and readable in your code
        public static HttpResponse Ok(byte[] body, string contentType)
            => new HttpResponse(200, contentType, body);

        public static HttpResponse NotFound()
            => new HttpResponse(404, "text/plain", System.Text.Encoding.UTF8.GetBytes("404 Not Found"));

        public static HttpResponse BadRequest()
            => new HttpResponse(400, "text/plain", System.Text.Encoding.UTF8.GetBytes("400 Bad Request"));
    }
}