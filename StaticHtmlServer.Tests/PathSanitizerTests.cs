using StaticHtmlServer.Domains.Paths;

namespace StaticHtmlServer.Tests
{
    public class PathSanitizerTests
    {
        [Fact]
        public void Sanitize_ValidRelativePath_ReturnsCombinedFullPath()
        {
            // arrange
            string testDir = @"C:\wwwroot";
            string requestedPath = "index.html";;
            IPathSanitizer testSanitizer = new PathSanitizer();

            // act
            string result = testSanitizer.Sanitize(testDir, requestedPath);
            // assert
            string expectedPath = @"C:\wwwroot\index.html";
            Assert.Equal(expectedPath, result);

        }
        [Fact]
        public void Sanitize_MaliciousPath_ThrowsArgumentException()
        {
            // arrange
            string testDir = @"C:\wwwroot";
            string maliciousPath = @"..\..\Windows\System32\cmd.exe";
            IPathSanitizer testSanitizer = new PathSanitizer();
            // act & assert
            Assert.Throws<ArgumentException>(() =>
            {
                testSanitizer.Sanitize(testDir, maliciousPath);
            });
        }
    }

}