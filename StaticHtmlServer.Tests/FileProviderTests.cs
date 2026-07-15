using System.Runtime.CompilerServices;
using StaticHtmlServer.Domains.Files;
namespace StaticHtmlServer.Tests
{
    public class FileProviderTest
    {
        [Fact]
        public void Exist_CheckIfValidFileExist()
        {
            // Given
            string testPath = @"C:\Users\User\Softwares\static-html-server\StaticHtmlServer\wwwroot\index.html";
            IFileProvider testFileProvider = new FileProvider();

            // When
            bool result = testFileProvider.Exists(testPath);
            // Then
            Assert.True(result);
        }
        [Fact]
        public void Exists_CheckIfInvalidFileExist()
        {
            // Given
            string invalidPath = @"C:\Users\User\Softwares\static-html-server\StaticHtmlServer\var\index.html";
            IFileProvider testFileProvider = new FileProvider();
            // When
            bool result = testFileProvider.Exists(invalidPath);
            // Then
            Assert.False(result);
        }
        [Fact]
        public void Exists_CheckIfNoPathExists_ThrowsException()
        {
            // Given
            string noPath = "";
            IFileProvider testFileProvider = new FileProvider();

            Assert.Throws<ArgumentException>(() =>
            {
                testFileProvider.Exists(noPath);
            });
        }
    }
}