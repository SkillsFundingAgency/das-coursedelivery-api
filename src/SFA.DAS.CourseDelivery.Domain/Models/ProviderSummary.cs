using SFA.DAS.CourseDelivery.Domain.Entities;

namespace SFA.DAS.CourseDelivery.Domain.Models
{
    public class ProviderSummary
    {
        public int Ukprn { get; set; }
        public string Name { get; set; }

        public static implicit operator ProviderSummary(Provider source)
        {
            return new ProviderSummary
            {
                Ukprn = source.Ukprn,
                Name = source.Name
            };
        }
    }
}