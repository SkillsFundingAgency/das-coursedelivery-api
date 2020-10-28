using System.Collections.Generic;
using System.Threading.Tasks;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.CourseDelivery.Domain.ImportTypes;

namespace SFA.DAS.CourseDelivery.Domain.Interfaces
{
    public interface IProviderRegistrationImportRepository
    {
        Task InsertMany(IEnumerable<ProviderRegistrationImport> providerImports);
        void DeleteAll();
        Task<IEnumerable<ProviderRegistrationImport>> GetAll();
        Task UpdateAddress(int ukprn, ContactAddress address, double lat, double lon);
    }
}