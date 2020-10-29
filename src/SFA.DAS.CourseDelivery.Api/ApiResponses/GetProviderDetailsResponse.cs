using SFA.DAS.CourseDelivery.Domain.Entities;

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

        public static implicit operator GetProviderDetailsResponse(Provider source)
        {
            return new GetProviderDetailsResponse
            {
                Id = source.Id,
                Ukprn = source.Ukprn,
                Name = source.Name,
                LearnerSatisfaction = source.LearnerSatisfaction,
                EmployerSatisfaction = source.EmployerSatisfaction,
                TradingName = source.TradingName,
                Email = source.Email,
                Website = source.Website,
                Phone = source.Phone
            };
        }
    }
}