using System.Collections.Generic;
using System.Threading.Tasks;
using SFA.DAS.CourseDelivery.Domain.Models;

namespace SFA.DAS.CourseDelivery.Domain.Interfaces
{
    public interface IProviderService
    {
        Task<IEnumerable<ProviderLocation>> GetProvidersByStandardId(int standardId);
        Task<Entities.ProviderStandard> GetProviderByUkprnAndStandard(int ukPrn, int standardId);
        Task<IEnumerable<Domain.Entities.NationalAchievementRateOverall>> GetOverallAchievementRates(string description);
        Task<IEnumerable<int>> GetStandardIdsByUkprn(int ukprn);
        Task<IEnumerable<ProviderLocation>> GetProvidersByStandardAndLocation(  int standardId, double lat, double lon, short querySortOrder);
    }
}