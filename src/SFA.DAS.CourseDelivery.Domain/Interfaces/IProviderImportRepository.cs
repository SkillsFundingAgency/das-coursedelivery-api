using System.Collections.Generic;
using System.Threading.Tasks;
using SFA.DAS.CourseDelivery.Domain.Entities;

namespace SFA.DAS.CourseDelivery.Domain.Interfaces
{
    public interface IProviderImportRepository
    {
        Task InsertMany(IEnumerable<ProviderImport> providerImports);
        void DeleteAll();
        Task<IEnumerable<ProviderImport>> GetAll();
        Task UpdateAddress(int ukprn, string postcode, double lat, double lon);
    }
}