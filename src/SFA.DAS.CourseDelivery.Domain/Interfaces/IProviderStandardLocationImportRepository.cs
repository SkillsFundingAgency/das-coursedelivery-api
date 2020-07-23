using System.Collections.Generic;
using System.Threading.Tasks;
using SFA.DAS.CourseDelivery.Domain.Entities;

namespace SFA.DAS.CourseDelivery.Domain.Interfaces
{
    public interface IProviderStandardLocationImportRepository
    {
        Task<IEnumerable<ProviderStandardLocationImport>> GetAll();
        void DeleteAll();
        Task InsertMany(IEnumerable<ProviderStandardLocationImport> items);
    }
}