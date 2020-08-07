using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.CourseDelivery.Domain.Interfaces;

namespace SFA.DAS.CourseDelivery.Data.Repository
{
    public class NationalAchievementRateImportRepository : INationalAchievementRateImportRepository
    {
        private readonly ICourseDeliveryDataContext _dataContext;

        public NationalAchievementRateImportRepository (ICourseDeliveryDataContext dataContext)
        {
            _dataContext = dataContext;
        }
        
        public async Task<IEnumerable<NationalAchievementRateImport>> GetAll()
        {
            var items = await _dataContext.NationalAchievementRateImports.ToListAsync();
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