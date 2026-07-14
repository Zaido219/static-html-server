namespace StaticHtmlServer.Domains.MimeTypes
{
    public interface IMimeTypeProvider
    {
        /// Given a file name or path
        /// return the appropriate mime type
        string GetMimeType(string pathOrFileName);
    }
}