using SFA.DAS.CourseDelivery.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace SFA.DAS.CourseDelivery.Api.ApiResponses
{
    public class GetEmployerFeedbackResponse
    {
        public List<GetFeedbackAttributeResponse> FeedbackAttributes { get; set; }
        public List<GetFeedbackRatingResponse> FeedbackRatings { get; set; }

        public GetEmployerFeedbackResponse(ProviderLocation provider)
        {
            FeedbackAttributes = provider?.FeedbackAttributes != null
                ? provider.FeedbackAttributes.Select(x => (GetFeedbackAttributeResponse)x).ToList()
                : new List<GetFeedbackAttributeResponse>();

            FeedbackRatings = provider?.FeedbackRating != null
                ? provider.FeedbackRating.Select(x => (GetFeedbackRatingResponse)x).ToList()
                : new List<GetFeedbackRatingResponse>();
        }
    }
}
