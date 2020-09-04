using System.Collections.Generic;
using System.Threading.Tasks;
using SFA.DAS.CourseDelivery.Domain.Entities;

namespace SFA.DAS.CourseDelivery.Domain.Interfaces
{
    public interface IProviderRepository
    {
        Task InsertFromImportTable();
        void DeleteAll();
        Task<IEnumerable<Provider>> GetByStandardId(int standardId);
        Task<Provider> GetByUkprn(int ukPrn);
        Task<IEnumerable<ProviderWithStandardAndLocation>> GetByStandardIdAndLocation(  int standardId, double lat,
            double lon, short sortOrder);

        Task<IEnumerable<ProviderWithStandardAndLocation>> GetProviderByStandardIdAndLocation(int ukprn, int standardId,
            double lat = 0, double lon = 0);
    }
}