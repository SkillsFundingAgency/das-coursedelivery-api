using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.CourseDelivery.Domain.Interfaces;

namespace SFA.DAS.CourseDelivery.Data.Repository.Import
{
    public class NationalAchievementRateImportRepository : INationalAchievementRateImportRepository
    {
        private readonly ICourseDeliveryDataContext _dataContext;

        public NationalAchievementRateImportRepository (ICourseDeliveryDataContext dataContext)
        {
            _dataContext = dataContext;
        }
        
        public async Task<IEnumerable<NationalAchievementRateImport>> GetAllWithAchievementData()
        {
            var items = await _dataContext.NationalAchievementRateImports
                .Where(c=>c.OverallCohort.HasValue || c.OverallAchievementRate.HasValue)
                .ToListAsync();
            return items;
        }

        public void DeleteAll()
        {
            _dataContext.NationalAchievementRateImports.RemoveRange(_dataContext.NationalAchievementRateImports);
            _dataContext.SaveChanges();
        }

        public async Task InsertMany(IEnumerable<NationalAchievementRateImport> items)
        {
            await _dataContext.NationalAchievementRateImports.AddRangeAsync(items);
            _dataContext.SaveChanges();
        }
        
    }
}