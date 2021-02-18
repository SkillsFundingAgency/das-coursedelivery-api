using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.CourseDelivery.Domain.Models;

namespace SFA.DAS.CourseDelivery.Api.ApiResponses
{
    public class GetProviderDetailsResponse
    {
        public long Id { get; set; }
        public int Ukprn { get; set; }
        public string Name { get; set; }
        public decimal? LearnerSatisfaction { get; set; }
        public decimal? EmployerSatisfaction { get; set; }
        public string TradingName { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string Phone { get; set; }

        public static implicit operator GetProviderDetailsResponse(ProviderSummary source)
        {
            return new GetProviderDetailsResponse
            {
                Ukprn = source.Ukprn,
                Name = source.Name,
                LearnerSatisfaction = 0,
                EmployerSatisfaction = 0,
                TradingName = source.TradingName,
                Email = source.Email,
                Website = source.ContactUrl,
                Phone = source.Phone
            };
        }
    }
}