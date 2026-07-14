using StaticHtmlServer.Domains.Files;
namespace StaticHtmlServer.Tests
{
    public class FileProviderTest
    {
        [Fact]
        public void Exist_CheckIfValidFileExist()
        {
            // Given
            string testPath = @"C:\Users\User\Softwares\static-html-server\StaticHtmlServer\index.html";
            IFileProvider testFileProvider = new FileProvider();

            // When
            bool result = testFileProvider.Exists(testPath);
            // Then
            Assert.True(result);
        }
    }
}