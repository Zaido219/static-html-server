using StaticHtmlServer.Domains.MimeTypes;
using StaticHtmlServer.Domains.Paths;
using StaticHtmlServer.Domains.Files;

namespace StaticHtmlServer.Infrastructure
{
    public class StaticFilePipeline : IRequestPipeline
    {
        private readonly IFileProvider _fileProvider;
        private readonly IMimeTypeProvider _mimeTypeProvider;
        private readonly IPathSanitizer _pathSanitizer;
        private readonly string rootDirectory = Path.Combine(AppContext.BaseDirectory, "wwwroot");
        // constructor
        public StaticFilePipeline(
            IFileProvider fileProvider,
            IMimeTypeProvider mimeTypeProvider,
            IPathSanitizer pathSanitizer
        )
        {
            _fileProvider = fileProvider;
            _mimeTypeProvider = mimeTypeProvider;
            _pathSanitizer = pathSanitizer;
        }

        public async Task<HttpResponse> ExecuteAsync(string rawPath)
        {
            string sanitizedPath = _pathSanitizer.Sanitize(rawPath, rootDirectory);
            // check if path exist
            if (!_fileProvider.Exists(sanitizedPath))
            {
                // short circuit
                return HttpResponse.NotFound();
            }
            // get mime type
            string mimeType = _mimeTypeProvider.GetMimeType(sanitizedPath);
            // read the file's raw bytes asynchronously
            using(Stream fileStream = _fileProvider.OpenRead(sanitizedPath))
            using(MemoryStream ms = new MemoryStream())
            {
                await fileStream.CopyToAsync(ms);
                byte[] fileBytes = ms.ToArray();

                //return as custom http response
                return HttpResponse.Ok(fileBytes, mimeType);
            }
        }
    }
};