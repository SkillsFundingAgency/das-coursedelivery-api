using SFA.DAS.CourseDelivery.Domain.Models;

namespace SFA.DAS.CourseDelivery.Api.ApiResponses
{
    public class GetProviderSummaryResponse
    {
        public int Ukprn { get ; set ; }

        public string Name { get ; set ; }
        public string TradingName { get; set; }
        public string ContactUrl { get ; set ; }
        public string Email { get ; set ; }
        public string Phone { get ; set ; }

        public static implicit operator GetProviderSummaryResponse(ProviderSummary source)
        {
            return new GetProviderSummaryResponse
            {
                Ukprn = source.Ukprn,
                Email = source.Email,
                Name = source.Name,
                Phone = source.Phone,
                TradingName = source.TradingName,
                ContactUrl = source.ContactUrl
            };
        }
    }
}