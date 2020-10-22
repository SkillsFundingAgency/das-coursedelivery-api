using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using SFA.DAS.Api.Common.Interfaces;
using SFA.DAS.CourseDelivery.Domain.Configuration;
using SFA.DAS.CourseDelivery.Domain.ImportTypes;
using SFA.DAS.CourseDelivery.Domain.Interfaces;

namespace SFA.DAS.CourseDelivery.Infrastructure.Api
{
    public class RoatpApiService : IRoatpApiService
    {
        private readonly HttpClient _client;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IAzureClientCredentialHelper _azureClientCredentialHelper;
        private readonly RoatpConfiguration _configuration;

        public RoatpApiService(IHttpClientFactory httpClientFactory, 
            IOptions<RoatpConfiguration> configuration, 
            IWebHostEnvironment hostingEnvironment,
            IAzureClientCredentialHelper azureClientCredentialHelper)
        {
            _client = httpClientFactory.CreateClient();
            _client.BaseAddress = new Uri(configuration.Value.Url);
            _hostingEnvironment = hostingEnvironment;
            _azureClientCredentialHelper = azureClientCredentialHelper;
            _configuration = configuration.Value;
        }

        public async Task<IEnumerable<ProviderRegistration>> GetProviderRegistrations()
        {
            await AddAuthenticationHeader();
            
            var response = await _client.GetAsync("v1/fat-data-export");
            
            response.EnsureSuccessStatusCode();
            
            var jsonResponse = await response.Content.ReadAsStringAsync();
            
            return JsonConvert.DeserializeObject<List<ProviderRegistration>>(jsonResponse);
        }

        public async Task<IEnumerable<ProviderRegistrationLookup>> GetProviderRegistrationLookupData(List<int> ukprns)
        {
            await AddAuthenticationHeader();
            
            var response = await _client.GetAsync($"v1/ukrlp/lookup/many?ukprns={string.Join("&ukprns=", ukprns)}");
            
            response.EnsureSuccessStatusCode();
            
            var jsonResponse = await response.Content.ReadAsStringAsync();
            
            return JsonConvert.DeserializeObject<List<ProviderRegistrationLookup>>(jsonResponse);
        }

        private async Task AddAuthenticationHeader()
        {
            if (!_hostingEnvironment.IsDevelopment())
            {
                var accessToken = await _azureClientCredentialHelper.GetAccessTokenAsync(_configuration.Identifier);
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);    
            }
        }
    }
}