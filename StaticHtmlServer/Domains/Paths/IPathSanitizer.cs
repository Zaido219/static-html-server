namespace StaticHtmlServer.Domains.Paths
{
    public interface IPathSanitizer
    {
        string Sanitize(string rootDir, string requestedPath);
    }
}