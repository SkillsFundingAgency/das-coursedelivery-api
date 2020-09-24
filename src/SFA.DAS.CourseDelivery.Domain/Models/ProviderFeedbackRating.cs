using SFA.DAS.CourseDelivery.Domain.Entities;

namespace SFA.DAS.CourseDelivery.Domain.Models
{
    public class ProviderFeedbackRating
    {
        public string FeedbackName { get ; set ; }
        public int FeedbackCount { get ; set ; }

        public static implicit operator ProviderFeedbackRating(ProviderRegistrationFeedbackRating source)
        {
            return new ProviderFeedbackRating
            {
                FeedbackCount = source.FeedbackCount,
                FeedbackName = source.FeedbackName
            };
        }

        public static implicit operator ProviderFeedbackRating(ProviderWithStandardAndLocation source)
        {
            if (!source.FeedbackCount.HasValue)
            {
                return null;
            }
            
            return new ProviderFeedbackRating
            {
                FeedbackCount = source.FeedbackCount.Value,
                FeedbackName = source.FeedbackName
            };
        }
    }
}