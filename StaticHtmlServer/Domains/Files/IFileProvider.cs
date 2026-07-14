namespace StaticHtmlServer.Domains.Files
{
    public interface IFileProvider
    {
        bool Exists(string path);
        Stream OpenRead(string physicalPath);
    }
}