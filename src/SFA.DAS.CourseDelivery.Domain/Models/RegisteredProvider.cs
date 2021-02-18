using System;

namespace SFA.DAS.CourseDelivery.Domain.Models
{
    public class RegisteredProvider
    {
        public int Ukprn { get; set; }
        public string Name { get; set; }
        public decimal? LearnerSatisfaction { get; set; }
        public decimal? EmployerSatisfaction { get; set; }
        public string TradingName { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string Phone { get; set; }
        
        public static implicit operator RegisteredProvider(Entities.Provider source)
        {
            return new RegisteredProvider
            {
                Ukprn = source.Ukprn,
                Name = source.Name,
                Email = source.Email,
                Phone = source.Phone,
                Website = source.Website,
                EmployerSatisfaction = source.EmployerSatisfaction,
                LearnerSatisfaction = source.LearnerSatisfaction,
                TradingName = source.TradingName
            };
        }

        public static implicit operator RegisteredProvider(Entities.ProviderRegistration source)
        {
            return new RegisteredProvider
            {
                Name = source.LegalName,
                TradingName = source.LegalName,
                Ukprn = source.Ukprn    
            };
        }
    }
}