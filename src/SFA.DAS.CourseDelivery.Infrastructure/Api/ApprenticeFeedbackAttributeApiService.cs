using SFA.DAS.CourseDelivery.Domain.ImportTypes;
using SFA.DAS.CourseDelivery.Domain.Interfaces;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SFA.DAS.CourseDelivery.Infrastructure.Api
{
    public class ApprenticeFeedbackAttributeApiService : IApprenticeFeedbackAttributesApiService
    {
        private readonly HttpClient _client;

        public ApprenticeFeedbackAttributeApiService(IHttpClientFactory httpClientFactory)
        {
            _client = httpClientFactory.CreateClient();
        }
        public async Task<IEnumerable<ApprenticeFeedbackAttribute>> GetApprenticeFeedbackAttributes()
        {
            var response = await _client.GetAsync("/ProviderAttributes");

            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<List<ApprenticeFeedbackAttribute>>(jsonResponse);
        }
    }
}
