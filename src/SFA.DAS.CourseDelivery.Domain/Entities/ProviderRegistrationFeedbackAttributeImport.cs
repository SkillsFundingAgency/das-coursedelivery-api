using SFA.DAS.CourseDelivery.Domain.ImportTypes;

namespace SFA.DAS.CourseDelivery.Domain.Entities
{
    public class ProviderRegistrationFeedbackAttributeImport : ProviderRegistrationFeedbackAttributeBase
    {
        public ProviderRegistrationFeedbackAttributeImport Map(int ukprn, ProviderAttribute source)
        {
            return new ProviderRegistrationFeedbackAttributeImport
            {
                Ukprn = ukprn,
                AttributeName = source.Name,
                Strength = source.Strengths,
                Weakness = source.Weaknesses
            };
        }
    }
}