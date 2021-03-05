using SFA.DAS.CourseDelivery.Domain.ImportTypes;

namespace SFA.DAS.CourseDelivery.Domain.Entities
{
    public class ProviderImport : ProviderBase
    {
        public static implicit operator ProviderImport(ImportTypes.Provider provider)
        {
            return new ProviderImport
            {
                Id = provider.Id,
                Ukprn = provider.Ukprn,
                Name = provider.Name,
                TradingName = provider.TradingName,
                Phone = provider.Phone,
                Website = provider.Website,
                Email = provider.Email,
                EmployerSatisfaction = provider.EmployerSatisfaction,
                LearnerSatisfaction = provider.LearnerSatisfaction,
                MarketingInfo = provider.MarketingInfo
            };
        }
    }
}