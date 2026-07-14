namespace StaticHtmlServer.Domains.Paths
{
    public interface IPathSanitizer
    {
        public string Sanitize(string rootDir, string requestedPath);
    }
}