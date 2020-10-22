using System.Collections.Generic;
using System.Threading.Tasks;
using SFA.DAS.CourseDelivery.Domain.ImportTypes;

namespace SFA.DAS.CourseDelivery.Domain.Interfaces
{
    public interface IRoatpApiService
    {
        Task<IEnumerable<ProviderRegistration>> GetProviderRegistrations();
        Task<IEnumerable<ProviderRegistrationLookup>> GetProviderRegistrationLookupData(IEnumerable<int> ukprns);
    }
}