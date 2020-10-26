using System.Collections.Generic;
using System.Linq;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.CourseDelivery.Domain.Models;

namespace SFA.DAS.CourseDelivery.Api.ApiResponses
{
    public class GetProviderResponse
    {
        public int Ukprn { get ; set ; }

        public string Name { get ; set ; }
        public string ContactUrl { get ; set ; }
        public string Email { get ; set ; }
        public string Phone { get ; set ; }

        public double ProviderDistance { get ; set ; }
        public List<GetNationalAchievementRateResponse> AchievementRates { get ; set ; }
        public List<GetDeliveryTypesResponse> DeliveryTypes { get ; set ; }
        public List<GetFeedbackAttributeResponse> FeedbackAttributes { get; set; }
        public List<GetFeedbackRatingResponse> FeedbackRatings { get; set; }

        public GetProviderResponse Map(ProviderLocation provider, short age = 0, short apprenticeshipLevel = 0)
        {
            var nationalAchievementRates = provider.AchievementRates.AsQueryable();

            if (apprenticeshipLevel != 0)
            {
                nationalAchievementRates = nationalAchievementRates.Where(c => c.Age.Equals((Age)age));
            }

            if (age != 0)
            {
                nationalAchievementRates = nationalAchievementRates.Where(c=> c.ApprenticeshipLevel.Equals((ApprenticeshipLevel)apprenticeshipLevel));
            }

            return new GetProviderResponse
            {
                Ukprn = provider.Ukprn,
                Name = provider.Name,
                Email = provider.Email,
                ContactUrl = provider.ContactUrl,
                Phone = provider.Phone,
                ProviderDistance = provider.ProviderDistanceInMiles,
                AchievementRates = nationalAchievementRates
                    .Select(c=>(GetNationalAchievementRateResponse)c).ToList(),
                DeliveryTypes = provider.DeliveryTypes.Select(c=>(GetDeliveryTypesResponse)c).ToList(),
                FeedbackAttributes = provider.FeedbackAttributes.Select(x=>(GetFeedbackAttributeResponse)x).ToList(),
                FeedbackRatings = provider.FeedbackRating.Select(x=>(GetFeedbackRatingResponse)x).ToList()
            };
        }
    }
}