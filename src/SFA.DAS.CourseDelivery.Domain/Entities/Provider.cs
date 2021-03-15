using System.Collections.Generic;

namespace SFA.DAS.CourseDelivery.Domain.Entities
{
    public class Provider : ProviderBase
    {
        public virtual ICollection<ProviderStandard> ProviderStandards { get ; set ; }
        public virtual ICollection<NationalAchievementRate> NationalAchievementRates { get ; set ; }
        public virtual ProviderRegistration ProviderRegistration { get ; set ; }
        public virtual IEnumerable<ProviderRegistrationFeedbackAttribute> ProviderRegistrationFeedbackAttributes { get ; set ; }
        public virtual IEnumerable<ProviderRegistrationFeedbackRating> ProviderRegistrationFeedbackRating { get ; set ; }
        

        public static implicit operator Provider(ProviderImport providerImport)
        {
            return new Provider
            {
                Email = providerImport.Email,
                Name = providerImport.Name,
                Phone = providerImport.Phone,
                Ukprn = providerImport.Ukprn,
                Website = providerImport.Website,
                EmployerSatisfaction = providerImport.EmployerSatisfaction,
                LearnerSatisfaction = providerImport.LearnerSatisfaction,
                TradingName = providerImport.TradingName,
                MarketingInfo = providerImport.MarketingInfo
            };
        }
    }
}