using SFA.DAS.CourseDelivery.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace SFA.DAS.CourseDelivery.Api.ApiResponses
{
    public class GetEmployerFeedbackResponse
    {
        private ProviderLocation provider;

        public GetEmployerFeedbackResponse(ProviderLocation provider)
        {
            this.provider = provider;
            FeedbackAttributes = provider.FeedbackAttributes.Select(x => (GetFeedbackAttributeResponse)x).ToList();
            FeedbackRatings = provider.FeedbackRating.Select(x => (GetFeedbackRatingResponse)x).ToList();
        }

        public List<GetFeedbackAttributeResponse> FeedbackAttributes { get; set; }
        public List<GetFeedbackRatingResponse> FeedbackRatings { get; set; }

    }
}
