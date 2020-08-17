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
    public class WhenGettingDataFromRoatpApi
    {
        [Test, AutoData]
        public async Task Then_The_Endpoint_Is_Called_And_Roatp_Data_Returned(
            string roatpKey,
            string roatpUrl,
            List<ProviderRegistration> importProviderRegistrations)
        {
            //Arrange
            roatpUrl = $"https://test.local/{roatpUrl}";
            var configuration = new Mock<IOptions<RoatpConfiguration>>();
            configuration.Setup(x => x.Value.Key).Returns(roatpKey);
            configuration.Setup(x => x.Value.Url).Returns(roatpUrl);
            var response = new HttpResponseMessage
            {
                Content = new StringContent(JsonConvert.SerializeObject(importProviderRegistrations)),
                StatusCode = HttpStatusCode.Accepted
            };
            var httpMessageHandler = MessageHandler.SetupMessageHandlerMock(response, new Uri(roatpUrl), roatpKey);
            var client = new HttpClient(httpMessageHandler.Object);
            var roatpApiService = new RoatpApiService(client, configuration.Object);
            
            //Act
            var providerRegistrations = await roatpApiService.GetProviderRegistrations();
            
            //Assert
            providerRegistrations.Should().BeEquivalentTo(importProviderRegistrations);
        }
        
        [Test, AutoData]
        public void Then_If_It_Is_Not_Successful_An_Exception_Is_Thrown(
            string roatpKey,
            string roatpUrl)
        {
            //Arrange
            roatpUrl = $"https://test.local/{roatpUrl}";
            var configuration = new Mock<IOptions<RoatpConfiguration>>();
            configuration.Setup(x => x.Value.Key).Returns(roatpKey);
            configuration.Setup(x => x.Value.Url).Returns(roatpUrl);
            var response = new HttpResponseMessage
            {
                Content = new StringContent(""),
                StatusCode = HttpStatusCode.BadRequest
            };
            var httpMessageHandler = MessageHandler.SetupMessageHandlerMock(response, new Uri(roatpUrl), roatpKey);
            var client = new HttpClient(httpMessageHandler.Object);
            var roatpApiService = new RoatpApiService(client, configuration.Object);
            
            //Act Assert
            Assert.ThrowsAsync<HttpRequestException>(() => roatpApiService.GetProviderRegistrations());
            
        }
    }
}