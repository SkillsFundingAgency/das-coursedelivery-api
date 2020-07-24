using SFA.DAS.CourseDelivery.Domain.ImportTypes;

namespace SFA.DAS.CourseDelivery.Domain.Entities
{
    public class ProviderImport : ProviderBase
    {
        public static implicit operator ProviderImport(Provider provider)
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
                NationalProvider = provider.NationalProvider,
                EmployerSatisfaction = provider.EmployerSatisfaction,
                LearnerSatisfaction = provider.LearnerSatisfaction
            };
        }
    }
}