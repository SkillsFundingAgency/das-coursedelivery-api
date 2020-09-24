namespace SFA.DAS.CourseDelivery.Domain.Entities
{
    public class ProviderRegistrationFeedbackRating : ProviderRegistrationFeedbackRatingBase
    {
        public virtual Provider Provider { get ; set ; }
        public virtual ProviderRegistration ProviderRegistration { get ; set ; }

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