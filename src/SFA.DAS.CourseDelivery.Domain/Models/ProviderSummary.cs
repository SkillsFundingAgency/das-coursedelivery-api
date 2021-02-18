using SFA.DAS.CourseDelivery.Domain.Entities;

namespace SFA.DAS.CourseDelivery.Domain.Models
{
    public class ProviderSummary
    {
        public int Ukprn { get; set; }
        public string Name { get; set; }
        public string Phone { get ; set ; }
        public string Email { get ; set ; }
        public string ContactUrl { get ; set ; }
        public string TradingName { get ; set ; }
        public ProviderSummaryAddress Address { get; set; }

        public static implicit operator ProviderSummary(Provider source)
        {
            return new ProviderSummary
            {
                Ukprn = source.Ukprn,
                Name = source.Name,
                Email = source.Email,
                Phone = source.Phone,
                TradingName = source.TradingName,
                ContactUrl = source.Website,
                Address = source.ProviderRegistration
            };
        }

        public static implicit operator ProviderSummary(ProviderRegistration source)
        {
            return new ProviderSummary
            {
                Ukprn = source.Ukprn,
                Name = source.LegalName,
                Email = string.Empty,
                Phone = string.Empty,
                TradingName = source.LegalName,
                ContactUrl = string.Empty,
                Address = source
            }; 
                
        }
    }
}