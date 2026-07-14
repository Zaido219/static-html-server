using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.StaticFiles;

namespace StaticHtmlServer.Domains.MimeTypes
{
    public class MimeTypeProvider : IMimeTypeProvider
    {
        private readonly FileExtensionContentTypeProvider _fileExtensionContentTypeProvider = new FileExtensionContentTypeProvider();
        public string GetMimeType(string pathOrFileName)
        {
            if (_fileExtensionContentTypeProvider.TryGetContentType(pathOrFileName, out string contentType))
            {
                return contentType;
            }
            // fallback if not supported
            return "application/octet-stream";
        }
    }
}