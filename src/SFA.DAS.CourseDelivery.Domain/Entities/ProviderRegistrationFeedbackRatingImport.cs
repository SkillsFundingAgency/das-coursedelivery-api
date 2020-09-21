using SFA.DAS.CourseDelivery.Domain.ImportTypes;

namespace SFA.DAS.CourseDelivery.Domain.Entities
{
    public class ProviderRegistrationFeedbackRatingImport : ProviderRegistrationFeedbackRatingBase
    {
        public ProviderRegistrationFeedbackRatingImport Map(int ukprn, FeedbackRating source)
        {
            return new ProviderRegistrationFeedbackRatingImport
            {
                Ukprn = ukprn,
                FeedbackCount = source.Value,
                FeedbackName = source.Key
            };
        }
    }
}