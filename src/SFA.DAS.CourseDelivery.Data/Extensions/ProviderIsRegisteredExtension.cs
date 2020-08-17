using System.Linq;
using SFA.DAS.CourseDelivery.Domain.ImportTypes;
using Provider = SFA.DAS.CourseDelivery.Domain.Entities.Provider;

namespace SFA.DAS.CourseDelivery.Data.Extensions
{
    public static class ProviderIsRegisteredExtension
    {
        public static IQueryable<Provider> FilterRegisteredProviders(
            this IQueryable<Provider> providers)
        {
            var filteredProviders = providers.Where(provider => 
                provider.ProviderRegistration.StatusId == RoatpTypeConstants.StatusOfActive &&
                provider.ProviderRegistration.ProviderTypeId == RoatpTypeConstants.ProviderTypeOfMainProvider);

            return filteredProviders;
        }
    }
}