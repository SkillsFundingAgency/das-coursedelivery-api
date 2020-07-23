namespace SFA.DAS.CourseDelivery.Domain.Entities
{
    public class ProviderStandardBase
    {
        public int Ukprn { get; set; }
        public int StandardId { get; set; }
        public string StandardInfoUrl { get; set; }
        public string ContactUrl { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}