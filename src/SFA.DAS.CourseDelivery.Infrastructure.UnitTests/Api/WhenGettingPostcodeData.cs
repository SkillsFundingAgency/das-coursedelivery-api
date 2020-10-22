using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoMoq;
using FluentAssertions;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Domain.Configuration;
using SFA.DAS.CourseDelivery.Domain.ImportTypes;
using SFA.DAS.CourseDelivery.Infrastructure.Api;

namespace SFA.DAS.CourseDelivery.Infrastructure.UnitTests.Api
{
    public class WhenGettingPostcodeData
    {
        [Test]
        public async Task Then_The_Endpoint_Is_Called_And_Data_Returned()
        {
            //Arrange
            var fixture = new Fixture().Customize(new AutoMoqCustomization{ConfigureMembers = true});
            var postcodeLookup = fixture.Create<PostcodeLookup>();
            var postcodes = fixture.Create<PostcodeLookupRequest>();
            var response = new HttpResponseMessage
            {
                Content = new StringContent(JsonConvert.SerializeObject(postcodeLookup)),
                StatusCode = HttpStatusCode.OK
            };
            var uri = new Uri(ConfigurationConstants.PostcodeLookupUrl+"postcodes");
            var httpMessageHandler = MessageHandler.SetupMessageHandlerMock(response, uri, HttpMethod.Post);
            var client = new HttpClient(httpMessageHandler.Object);
            
            var apiService = new PostcodeApiService(client);

            //Act
            var postcodeData = await apiService.GetPostcodeData(postcodes);
            
            //Assert
            httpMessageHandler.Protected()
                .Verify<Task<HttpResponseMessage>>(
                    "SendAsync", Times.Once(),
                    ItExpr.Is<HttpRequestMessage>(c =>
                        c.Method.Equals(HttpMethod.Post)
                        && c.RequestUri.Equals(uri)),
                    ItExpr.IsAny<CancellationToken>()
                );
            postcodeData.Should().BeEquivalentTo(postcodeLookup);
        }
        
        [Test]
        public void Then_If_It_Is_Not_Successful_An_Exception_Is_Thrown()
        {
            //Arrange
            var fixture = new Fixture().Customize(new AutoMoqCustomization{ConfigureMembers = true});
            var postcodes = fixture.Create<PostcodeLookupRequest>();
            var response = new HttpResponseMessage
            {
                Content = new StringContent(""),
                StatusCode = HttpStatusCode.BadRequest
            };
            var uri = new Uri(ConfigurationConstants.PostcodeLookupUrl+"postcodes");
            var httpMessageHandler = MessageHandler.SetupMessageHandlerMock(response, uri, HttpMethod.Post);
            var client = new HttpClient(httpMessageHandler.Object);
            
            var apiService = new PostcodeApiService(client);
            
            //Act Assert
            Assert.ThrowsAsync<HttpRequestException>(() => apiService.GetPostcodeData(postcodes));
            
        }
    }
}