using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SFA.DAS.CourseDelivery.Domain.Configuration;
using SFA.DAS.CourseDelivery.Domain.ImportTypes;
using SFA.DAS.CourseDelivery.Domain.Interfaces;

namespace SFA.DAS.CourseDelivery.Infrastructure.Api
{
    public class CourseDirectoryService : ICourseDirectoryService
    {
        private readonly HttpClient _client;
        private readonly CourseDirectoryConfiguration _configuration;

        public CourseDirectoryService(HttpClient client, IOptions<CourseDirectoryConfiguration> configuration)
        {
            _client = client;
            _configuration = configuration.Value;
        }

        public async Task<IEnumerable<Provider>> GetProviderCourseInformation()
        {
            AddHeaders();
            
            var response = await _client.GetAsync(new Uri(_configuration.Url));
            
            response.EnsureSuccessStatusCode();
            
            var jsonResponse = await response.Content.ReadAsStringAsync();
            
            return JsonConvert.DeserializeObject<List<Provider>>(jsonResponse);
        }
        
        private void AddHeaders()
        {
            _client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _configuration.Key);
        }
    }
}