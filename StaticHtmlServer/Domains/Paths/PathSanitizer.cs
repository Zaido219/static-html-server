using System;
using System.IO;
using System.Runtime.InteropServices;

namespace StaticHtmlServer.Domains.Paths
{
    public class PathSanitizer : IPathSanitizer
    {
       public string Sanitize(string rootDir, string requestedPath)
        {
            // no path
            if (string.IsNullOrWhiteSpace(requestedPath))
            {
                throw new ArgumentException("Path cannot be empty or whitespace.", nameof(requestedPath));
            }
            // combine paths
            string combinePaths = Path.Combine(rootDir, requestedPath);
            // resolve full path
            string fullPath = Path.GetFullPath(combinePaths);
            // ensure absolute root path
            string fullRootPath = Path.GetFullPath(rootDir);
            // ensure the root path  has a trailing separator if not append it
            if (!fullRootPath.EndsWith(Path.DirectorySeparatorChar))
            {
                fullRootPath += Path.DirectorySeparatorChar;
            }
            // compare
            if (!fullPath.StartsWith(rootDir, StringComparison.OrdinalIgnoreCase)) // reject if requested path does not start with the root dir
            {
                throw new ArgumentException("Invalid Path");
            }
            else
            {
                return fullPath;
            }
        }
    }
}
