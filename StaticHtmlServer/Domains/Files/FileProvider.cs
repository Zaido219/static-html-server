using System;
using System.IO;
using System.Text;

namespace StaticHtmlServer.Domains.Files
{
    public class FileProvider : IFileProvider
    {
        public bool Exists(string path)
        {
            // no path
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException("Path cannot be empty or whitespace", nameof(path));
            }
            
            if (File.Exists(path))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public Stream OpenRead(string physicalPath)
        {
            return File.OpenRead(physicalPath);
        }
    }
}