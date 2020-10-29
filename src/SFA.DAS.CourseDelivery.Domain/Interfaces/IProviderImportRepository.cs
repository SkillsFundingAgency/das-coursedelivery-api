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
        
    }
}