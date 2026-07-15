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
        private readonly string rootDirectory = @"C:\wwwroot";
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

        public Task<HttpResponse> ExecuteAsync(string rawPath)
        {
            string sanitizedPath = _pathSanitizer.Sanitize(rawPath, rootDirectory);
            // check if path exist
            if (_fileProvider.Exists(sanitizedPath))
            {
                
            }
        }
    }
};