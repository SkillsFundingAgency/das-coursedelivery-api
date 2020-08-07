using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.CourseDelivery.Domain.Interfaces;

namespace SFA.DAS.CourseDelivery.Data.Repository
{
    public class NationalAchievementRateImportRepository : INationalAchievementRateImportRepository
    {
        private readonly ICourseDeliveryDataContext _dataDataContext;

        public NationalAchievementRateImportRepository (ICourseDeliveryDataContext dataContext)
        {
            _dataDataContext = dataContext;
        }
        
        public async Task<IEnumerable<NationalAchievementRateImport>> GetAll()
        {
            var providerStandardImports = await _dataDataContext.NationalAchievementRateImports.ToListAsync();
            return providerStandardImports;
        }

        public void DeleteAll()
        {
            _dataDataContext.NationalAchievementRateImports.RemoveRange(_dataDataContext.NationalAchievementRateImports);
            _dataDataContext.SaveChanges();
        }

        public async Task InsertMany(IEnumerable<NationalAchievementRateImport> items)
        {
            await _dataDataContext.NationalAchievementRateImports.AddRangeAsync(items);
            _dataDataContext.SaveChanges();
        }
        
    }
}