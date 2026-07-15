using System.Reflection.Metadata;
using StaticHtmlServer.Domains.MimeTypes;
using Xunit.Sdk;

namespace StaticHtmlServer.Tests
{
    public class MimeTypeProviderTests
    {
        [Theory]
        [InlineData("index.html", "text/html")]
        [InlineData("styles.css", "text/css")]
        [InlineData("app.js", "application/javascript")]
        [InlineData("logo.png", "image/png")]
        
        public void GetMimeType_WithKnownExtensions_ReturnCorrectMimeType(string fileName, string expectedMimeType)
        {
            //given
            IMimeTypeProvider testProvider = new MimeTypeProvider();
            // when
            string result = testProvider.GetMimeType(fileName);
            //then
            Assert.Equal(expectedMimeType, result);
        }
        [Fact]
        public void GetMimeType_MissingOrUnknownExtension_ReturnsDefaultMimeType()
        {
            // given
            IMimeTypeProvider testProvider = new MimeTypeProvider();
            string fileName = "archive.7z";
            // when
            string result = testProvider.GetMimeType(fileName);
            // then
            Assert.Equal("application/octet-stream", result);
        }
    }
}