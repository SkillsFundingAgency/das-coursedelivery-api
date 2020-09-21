namespace SFA.DAS.CourseDelivery.Domain.Entities
{
    public class ProviderRegistrationFeedbackRating : ProviderRegistrationFeedbackRatingBase
    {
        public static implicit operator ProviderRegistrationFeedbackRating(
            ProviderRegistrationFeedbackRatingImport source)
        {
            return new ProviderRegistrationFeedbackRating
            {
                Ukprn = source.Ukprn,
                FeedbackCount = source.FeedbackCount,
                FeedbackName = source.FeedbackName
            };
        }
    }
}