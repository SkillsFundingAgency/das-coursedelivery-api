using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Domain.Configuration;
using SFA.DAS.CourseDelivery.Domain.ImportTypes;
using SFA.DAS.CourseDelivery.Infrastructure.Api;

namespace SFA.DAS.CourseDelivery.Infrastructure.UnitTests.Api
{
    public class WhenGettingDataFromCourseDirectory
    {
        [Test, AutoData]
        public async Task Then_The_Endpoint_Is_Called_And_CourseDirectory_Data_Returned(
            string apiKey,
            string coursesUrl,
            List<Provider> importProviders)
        {
            //Arrange
            coursesUrl = $"https://test.local/{coursesUrl}";
            var configuration = new Mock<IOptions<CourseDirectoryConfiguration>>();
            configuration.Setup(x => x.Value.Key).Returns(apiKey);
            configuration.Setup(x => x.Value.Url).Returns(coursesUrl);
            var response = new HttpResponseMessage
            {
                Content = new StringContent(JsonConvert.SerializeObject(importProviders)),
                StatusCode = HttpStatusCode.Accepted
            };
            var httpMessageHandler = MessageHandler.SetupMessageHandlerMock(response, new Uri(coursesUrl), apiKey);
            var client = new HttpClient(httpMessageHandler.Object);
            var apprenticeshipService = new CourseDirectoryService(client, configuration.Object);
            
            //Act
            var providers = await apprenticeshipService.GetProviders();
            
            //Assert
            providers.Should().BeEquivalentTo(importProviders);
        }
        
        [Test, AutoData]
        public void Then_If_It_Is_Not_Successful_An_Exception_Is_Thrown(
            string apiKey,
            string coursesUrl)
        {
            //Arrange
            coursesUrl = $"https://test.local/{coursesUrl}";
            var configuration = new Mock<IOptions<CourseDirectoryConfiguration>>();
            configuration.Setup(x => x.Value.Key).Returns(apiKey);
            configuration.Setup(x => x.Value.Url).Returns(coursesUrl);
            var response = new HttpResponseMessage
            {
                Content = new StringContent(""),
                StatusCode = HttpStatusCode.BadRequest
            };
            var httpMessageHandler = MessageHandler.SetupMessageHandlerMock(response, new Uri(coursesUrl), apiKey);
            var client = new HttpClient(httpMessageHandler.Object);
            var apprenticeshipService = new CourseDirectoryService(client, configuration.Object);
            
            //Act Assert
            Assert.ThrowsAsync<HttpRequestException>(() => apprenticeshipService.GetProviders());
            
        }
    }
}