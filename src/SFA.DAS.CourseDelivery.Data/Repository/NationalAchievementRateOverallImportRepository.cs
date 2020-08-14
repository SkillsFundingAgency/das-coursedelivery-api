using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.CourseDelivery.Domain.Interfaces;

namespace SFA.DAS.CourseDelivery.Data.Repository
{
    public class NationalAchievementRateOverallImportRepository : INationalAchievementRateOverallImportRepository
    {
        private readonly ICourseDeliveryDataContext _dataContext;
        public NationalAchievementRateOverallImportRepository (ICourseDeliveryDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IEnumerable<NationalAchievementRateOverallImport>> GetAllWithAchievementData()
        {
            var items = await _dataContext.NationalAchievementRateOverallImports
                .Where(c=>c.OverallCohort.HasValue || c.OverallAchievementRate.HasValue)
                .ToListAsync();
            return items;
        }

        public async Task InsertMany(IEnumerable<NationalAchievementRateOverallImport> items)
        {
            await _dataContext.NationalAchievementRateOverallImports.AddRangeAsync(items);
            _dataContext.SaveChanges();
        }

        public void DeleteAll()
        {
            _dataContext.NationalAchievementRateOverallImports.RemoveRange(_dataContext.NationalAchievementRateOverallImports);
            _dataContext.SaveChanges();
        }
    }
}