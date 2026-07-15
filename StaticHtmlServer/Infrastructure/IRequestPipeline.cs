namespace StaticHtmlServer.Infrastructure
{
    public interface IRequestPipeline
    {
        Task<HttpResponse> ExecuteAsync(string rawPath);
    }
}