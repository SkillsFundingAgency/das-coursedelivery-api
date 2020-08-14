using System.Collections.Generic;
using System.Threading.Tasks;
using SFA.DAS.CourseDelivery.Domain.Entities;

namespace SFA.DAS.CourseDelivery.Domain.Interfaces
{
    public interface IProviderRegistrationImportRepository
    {
        Task InsertMany(IEnumerable<ProviderRegistrationImport> providerImports);
        void DeleteAll();
        Task<IEnumerable<ProviderRegistrationImport>> GetAll();
    }
}