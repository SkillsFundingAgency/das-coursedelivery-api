using System.Collections.Generic;
using System.Threading.Tasks;
using SFA.DAS.CourseDelivery.Domain.Entities;

namespace SFA.DAS.CourseDelivery.Domain.Interfaces
{
    public interface IProviderStandardImportRepository
    {
        Task<IEnumerable<ProviderStandardImport>> GetAll();
        void DeleteAll();
        Task InsertMany(IEnumerable<ProviderStandardImport> providerStandardImports);
    }
}