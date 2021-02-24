using System.Collections.Generic;
using System.Linq;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.CourseDelivery.Domain.Models;

namespace SFA.DAS.CourseDelivery.Api.ApiResponses
{
    public class GetProviderDetailResponse : GetProviderSummaryResponse
    {
        public string StandardInfoUrl { get; set; }
        public GetProviderHeadOfficeAddress ProviderAddress { get; set; }
        public List<GetNationalAchievementRateResponse> AchievementRates { get ; set ; }
        public List<GetDeliveryTypesResponse> DeliveryTypes { get ; set ; }
        public List<GetFeedbackAttributeResponse> FeedbackAttributes { get; set; }
        public List<GetFeedbackRatingResponse> FeedbackRatings { get; set; }

        public GetProviderDetailResponse Map(ProviderLocation provider, short age = 0)
        {
            var nationalAchievementRates = provider.AchievementRates.AsQueryable();

            if (age != 0)
            {
                nationalAchievementRates = nationalAchievementRates.Where(c => c.Age.Equals((Age)age));
            }

            return new GetProviderDetailResponse
            {
                Ukprn = provider.Ukprn,
                Name = provider.Name,
                TradingName = provider.TradingName,
                Email = provider.Email,
                ContactUrl = provider.ContactUrl,
                Phone = provider.Phone,
                StandardInfoUrl = provider.StandardInfoUrl,
                ProviderAddress =  provider.Address,
                AchievementRates = nationalAchievementRates
                    .Select(c=>(GetNationalAchievementRateResponse)c).ToList(),
                DeliveryTypes = provider.DeliveryTypes.Select(c=>(GetDeliveryTypesResponse)c).ToList(),
                FeedbackAttributes = provider.FeedbackAttributes.Select(x=>(GetFeedbackAttributeResponse)x).ToList(),
                FeedbackRatings = provider.FeedbackRating.Select(x=>(GetFeedbackRatingResponse)x).ToList()
            };
        }
    }
}