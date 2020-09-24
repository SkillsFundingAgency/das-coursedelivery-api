namespace SFA.DAS.CourseDelivery.Domain.Entities
{
    public class ProviderRegistrationFeedbackAttribute : ProviderRegistrationFeedbackAttributeBase
    {
        public virtual ProviderRegistration ProviderRegistration { get; set; }
        public virtual Provider Provider { get; set; }
        public static implicit operator ProviderRegistrationFeedbackAttribute(
            ProviderRegistrationFeedbackAttributeImport source)
        {
            return  new ProviderRegistrationFeedbackAttribute
            {
                Ukprn = source.Ukprn,
                Strength = source.Strength,
                Weakness = source.Weakness,
                AttributeName = source.AttributeName
            };
        }
    }
}