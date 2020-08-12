using System.Collections.Generic;
using System.Linq;

namespace SFA.DAS.CourseDelivery.Api.ApiResponses
{
    public class GetCourseProviderResponse
    {
        public string Name { get ; set ; }
        public int Ukprn { get ; set ; }
        public string ContactUrl { get ; set ; }
        public string Email { get ; set ; }
        public string Phone { get ; set ; }

        public List<GetNationalAchievementRateResponse> AchievementRates { get ; set ; }
        public static implicit operator GetCourseProviderResponse(Application.Provider.Queries.Provider.GetProviderResponse source)
        {
            return new GetCourseProviderResponse
            {
                Email = source.ProviderStandardContact.Email,
                Phone = source.ProviderStandardContact.Phone,
                ContactUrl = source.ProviderStandardContact.ContactUrl,
                Name = source.ProviderStandardContact.Provider.Name,
                Ukprn = source.ProviderStandardContact.Provider.Ukprn,
                AchievementRates = source.ProviderStandardContact.NationalAchievementRate.Select(c=>(GetNationalAchievementRateResponse)c).ToList()
            };
        }
    }
}