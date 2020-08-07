using System.Collections.Generic;
using System.Threading.Tasks;
using SFA.DAS.CourseDelivery.Domain.Entities;

namespace SFA.DAS.CourseDelivery.Domain.Interfaces
{
    public interface INationalAchievementRateImportRepository
    {
        Task<IEnumerable<NationalAchievementRateImport>> GetAll();
        void DeleteAll();
        Task InsertMany(IEnumerable<NationalAchievementRateImport> items);
    }
}