using System.Collections.Generic;
using System.Threading.Tasks;
using SFA.DAS.CourseDelivery.Domain.Entities;

namespace SFA.DAS.CourseDelivery.Domain.Interfaces
{
    public interface IProviderStandardRepository
    {
        void DeleteAll();
        Task InsertFromImportTable();
        Task<ProviderStandard> GetByUkprnAndStandard(int ukPrn, int standardId);
        Task<IEnumerable<int>> GetCoursesByUkprn(int ukPrn);
    }
}