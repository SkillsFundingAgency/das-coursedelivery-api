namespace SFA.DAS.CourseDelivery.Domain.Entities
{
    public class ProviderRegistrationFeedbackAttributeBase
    {
        public int Ukprn { get; set; }
        public string AttributeName { get; set; }
        public int Strength { get; set; }
        public int Weakness { get; set; }
    }
}