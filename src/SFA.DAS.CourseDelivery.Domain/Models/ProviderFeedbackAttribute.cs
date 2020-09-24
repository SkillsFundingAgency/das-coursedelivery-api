using SFA.DAS.CourseDelivery.Domain.Entities;

namespace SFA.DAS.CourseDelivery.Domain.Models
{
    public class ProviderFeedbackAttribute
    {
        public int Weakness { get ; set ; }
        public int Strength { get ; set ; }
        public string AttributeName { get ; set ; }

        public static implicit operator ProviderFeedbackAttribute(ProviderRegistrationFeedbackAttribute source)
        {
            return new ProviderFeedbackAttribute
            {
                AttributeName = source.AttributeName,
                Strength = source.Strength,
                Weakness = source.Weakness,
            };
        }

        public static implicit operator ProviderFeedbackAttribute(ProviderWithStandardAndLocation source)
        {
            return new ProviderFeedbackAttribute
            {
                AttributeName = source.AttributeName,
                Strength = source.Strength ?? 0,
                Weakness = source.Weakness ?? 0,
            };
        }
    }
}