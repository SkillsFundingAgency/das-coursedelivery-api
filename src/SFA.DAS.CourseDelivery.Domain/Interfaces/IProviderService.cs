using System.Collections.Generic;
using System.Threading.Tasks;

namespace SFA.DAS.CourseDelivery.Domain.Interfaces
{
    public interface IProviderService
    {
        Task<IEnumerable<Domain.Entities.Provider>> GetProvidersByStandardId(int standardId);
        Task<Domain.Entities.ProviderStandard> GetProviderByUkprnAndStandard(int ukPrn, int standardId);
        Task<IEnumerable<int>> GetStandardIdsByUkprn(int ukprn);
    }
}