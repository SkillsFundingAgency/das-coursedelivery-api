using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using SFA.DAS.CourseDelivery.Domain.Entities;

namespace SFA.DAS.CourseDelivery.Api.ApiResponses
{
    public class GetProviderResponse
    {
        public int Ukprn { get ; set ; }

        public string Name { get ; set ; }
        public string Email { get ; set ; }
        public string Website { get ; set ; }
        public string Phone { get ; set ; }
        public IEnumerable<GetProviderStandardResponse> ProviderStandard { get; set; } 

        public static implicit operator GetProviderResponse(Provider provider)
        {
            return new GetProviderResponse
            {
                Ukprn = provider.Ukprn,
                Name = provider.Name,
                ProviderStandard = provider.ProviderStandards.Select(c=>(GetProviderStandardResponse)c)
            };
        }
    }
}