using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SFA.DAS.CourseDelivery.Domain.Configuration;
using SFA.DAS.CourseDelivery.Domain.ImportTypes;
using SFA.DAS.CourseDelivery.Domain.Interfaces;

namespace SFA.DAS.CourseDelivery.Infrastructure.Api
{
    public class PostcodeApiService : IPostcodeApiService
    {
        private readonly HttpClient _client;

        public PostcodeApiService (HttpClient client)
        {
            _client = client;
            _client.BaseAddress = new Uri(ConfigurationConstants.PostcodeLookupUrl);
        }


        public async Task<PostcodeLookup> GetPostcodeData(PostcodeLookupRequest postcodeLookupRequest)
        {
            var stringContent =
                new StringContent(JsonConvert.SerializeObject(postcodeLookupRequest), Encoding.UTF8, "application/json");
            
            var response = await _client.PostAsync($"postcodes",stringContent);

            response.EnsureSuccessStatusCode();
            
            var jsonResponse = await response.Content.ReadAsStringAsync();
            
            return JsonConvert.DeserializeObject<PostcodeLookup>(jsonResponse);
        }
    }
}