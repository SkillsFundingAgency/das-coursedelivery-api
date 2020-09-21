using SFA.DAS.CourseDelivery.Domain.Models;

namespace SFA.DAS.CourseDelivery.Api.ApiResponses
{
    public class GetFeedbackRatingResponse
    {
        public int FeedbackCount { get ; set ; }
        public string FeedbackName { get ; set ; }

        public static implicit operator GetFeedbackRatingResponse(ProviderFeedbackRating source)
        {
            return new GetFeedbackRatingResponse
            {
                FeedbackName = source.FeedbackName,
                FeedbackCount = source.FeedbackCount
            };
        }
    }
}