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

        public List<GetNationalAchievementRateResponse> AchievementRates { get ; set ; }

        public static implicit operator GetProviderResponse(Provider provider)
        {
            return new GetProviderResponse
            {
                Ukprn = provider.Ukprn,
                Name = provider.Name,
                AchievementRates = provider.NationalAchievementRates
                    .Where(c=>
                        c.Age.Equals(Age.AllAges) 
                        && c.ApprenticeshipLevel.Equals(ApprenticeshipLevel.AllLevels))
                    .Select(c=>(GetNationalAchievementRateResponse)c).ToList()
            };
        }
    }
}