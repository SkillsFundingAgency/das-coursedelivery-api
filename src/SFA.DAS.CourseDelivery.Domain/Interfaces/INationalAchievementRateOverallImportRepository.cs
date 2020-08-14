using System.Collections.Generic;
using System.Threading.Tasks;
using SFA.DAS.CourseDelivery.Domain.Entities;

namespace SFA.DAS.CourseDelivery.Domain.Interfaces
{
    public interface INationalAchievementRateOverallImportRepository
    {
        Task<IEnumerable<NationalAchievementRateOverallImport>> GetAllWithAchievementData();
        Task InsertMany(IEnumerable<NationalAchievementRateOverallImport> items);
        void DeleteAll();
    }
}