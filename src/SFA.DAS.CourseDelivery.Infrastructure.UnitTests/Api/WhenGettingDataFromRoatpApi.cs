using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Domain.Configuration;
using SFA.DAS.CourseDelivery.Domain.ImportTypes;
using SFA.DAS.CourseDelivery.Infrastructure.Api;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CourseDelivery.Infrastructure.UnitTests.Api
{
    public class WhenGettingProviderLookupDataFromRoatpApi
    {
        [Test]
        public async Task Then_The_Endpoint_Is_Called_And_Data_Returned()
        {
            //Arrange
            var fixture = new Fixture().Customize(new AutoMoqCustomization{ConfigureMembers = true});
            var importProviderRegistrations = fixture.Create<ProviderRegistrationLookup>();
            var config = fixture.Freeze<RoatpConfiguration>();
            config.Url = "https://test.local/api/";
            var ukprns = fixture.CreateMany<int>().ToList();
            var response = new HttpResponseMessage
            {
                Content = new StringContent(JsonConvert.SerializeObject(importProviderRegistrations)),
                StatusCode = HttpStatusCode.Accepted
            };
            var httpMessageHandler = MessageHandler.SetupMessageHandlerMock(response, new Uri(config.Url+$"v1/ukrlp/lookup/many?ukprns={string.Join("&ukprns=", ukprns)}"), HttpMethod.Get);
            var mockHttpClientFactory = fixture.Freeze<Mock<IHttpClientFactory>>();
            mockHttpClientFactory
                .Setup(factory => factory.CreateClient(It.IsAny<string>()))
                .Returns(new HttpClient(httpMessageHandler.Object));
            
            var roatpApiService = fixture.Create<RoatpApiService>();

            //Act
            var providerRegistrations = await roatpApiService.GetProviderRegistrationLookupData(ukprns);
            
            //Assert
            providerRegistrations.Should().BeEquivalentTo(importProviderRegistrations);
        }
        
        [Test]
        public void Then_If_It_Is_Not_Successful_An_Exception_Is_Thrown()
        {
            //Arrange
            var fixture = new Fixture().Customize(new AutoMoqCustomization{ConfigureMembers = true});
            var config = fixture.Freeze<RoatpConfiguration>();
            var ukprns = fixture.CreateMany<int>().ToList();
            config.Url = "https://test.local/api/";
            var response = new HttpResponseMessage
            {
                Content = new StringContent(""),
                StatusCode = HttpStatusCode.BadRequest
            };
            var httpMessageHandler = MessageHandler.SetupMessageHandlerMock(response, new Uri(config.Url+$"v1/ukrlp/lookup/many?ukprns={string.Join("&ukprns=", ukprns)}"), HttpMethod.Get);
            var mockHttpClientFactory = fixture.Freeze<Mock<IHttpClientFactory>>();
            mockHttpClientFactory
                .Setup(factory => factory.CreateClient(It.IsAny<string>()))
                .Returns(new HttpClient(httpMessageHandler.Object));

            var roatpApiService = fixture.Create<RoatpApiService>();
            
            //Act Assert
            Assert.ThrowsAsync<HttpRequestException>(() => roatpApiService.GetProviderRegistrationLookupData(ukprns));
        }
    }
    public class WhenGettingDataFromRoatpApi
    {
        [Test]
        public async Task Then_The_Endpoint_Is_Called_And_Roatp_Data_Returned()
        {
            //Arrange
            var fixture = new Fixture().Customize(new AutoMoqCustomization{ConfigureMembers = true});
            var importProviderRegistrations = fixture.Create<List<ProviderRegistration>>();
            var config = fixture.Freeze<RoatpConfiguration>();
            config.Url = "https://test.local/api/";
            var response = new HttpResponseMessage
            {
                Content = new StringContent(JsonConvert.SerializeObject(importProviderRegistrations)),
                StatusCode = HttpStatusCode.Accepted
            };
            var httpMessageHandler = MessageHandler.SetupMessageHandlerMock(response, new Uri(config.Url+"v1/fat-data-export"), HttpMethod.Get);
            var mockHttpClientFactory = fixture.Freeze<Mock<IHttpClientFactory>>();
            mockHttpClientFactory
                .Setup(factory => factory.CreateClient(It.IsAny<string>()))
                .Returns(new HttpClient(httpMessageHandler.Object));
            
            var roatpApiService = fixture.Create<RoatpApiService>();

            //Act
            var providerRegistrations = await roatpApiService.GetProviderRegistrations();
            
            //Assert
            providerRegistrations.Should().BeEquivalentTo(importProviderRegistrations);
        }
        
        [Test]
        public void Then_If_It_Is_Not_Successful_An_Exception_Is_Thrown()
        {
            //Arrange
            var fixture = new Fixture().Customize(new AutoMoqCustomization{ConfigureMembers = true});
            var config = fixture.Freeze<RoatpConfiguration>();
            config.Url = "https://test.local/api/";
            var response = new HttpResponseMessage
            {
                Content = new StringContent(""),
                StatusCode = HttpStatusCode.BadRequest
            };
            var httpMessageHandler = MessageHandler.SetupMessageHandlerMock(response, new Uri(config.Url+"v1/fat-data-export"), HttpMethod.Get);
            var mockHttpClientFactory = fixture.Freeze<Mock<IHttpClientFactory>>();
            mockHttpClientFactory
                .Setup(factory => factory.CreateClient(It.IsAny<string>()))
                .Returns(new HttpClient(httpMessageHandler.Object));

            var roatpApiService = fixture.Create<RoatpApiService>();
            
            //Act Assert
            Assert.ThrowsAsync<HttpRequestException>(() => roatpApiService.GetProviderRegistrations());
        }
    }
}
