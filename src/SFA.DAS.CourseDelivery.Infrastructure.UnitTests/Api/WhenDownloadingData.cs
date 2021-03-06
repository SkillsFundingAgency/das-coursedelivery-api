using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Infrastructure.Api;

namespace SFA.DAS.CourseDelivery.Infrastructure.UnitTests.Api
{
    public class WhenDownloadingData
    {
        [Test, AutoData]
        public async Task Then_The_File_Is_Downloaded_From_The_Url(string content)
        {
            //Arrange
            var response = new HttpResponseMessage
            {
                Content = new StreamContent(new MemoryStream(Encoding.UTF8.GetBytes(content))),
                StatusCode = HttpStatusCode.Accepted
            };
            var downloadUrl = "https://test";
            var httpMessageHandler = MessageHandler.SetupMessageHandlerMock(response, new Uri(downloadUrl), HttpMethod.Get);
            var client = new HttpClient(httpMessageHandler.Object);
            var larsDataDownloadService = new DataDownloadService(client);

            //Act
            var actual = await larsDataDownloadService.GetFileStream(downloadUrl);

            //Assert
            var reader = new StreamReader(actual);
            var actualContent = reader.ReadToEnd();
            actualContent.Should().Be(content);
        }

        [Test]
        public void Then_If_It_Is_Not_Successful_An_Exception_Is_Thrown()
        {
            //Arrange
            var response = new HttpResponseMessage
            {
                Content = new StringContent(""),
                StatusCode = HttpStatusCode.BadRequest
            };
            var downloadUrl = "https://test.zip";
            var httpMessageHandler = MessageHandler.SetupMessageHandlerMock(response, new Uri(downloadUrl), HttpMethod.Get);
            var client = new HttpClient(httpMessageHandler.Object);
            var larsDataDownloadService = new DataDownloadService(client);

            //Act Assert
            Assert.ThrowsAsync<HttpRequestException>(() => larsDataDownloadService.GetFileStream(downloadUrl));
        }
    }
}