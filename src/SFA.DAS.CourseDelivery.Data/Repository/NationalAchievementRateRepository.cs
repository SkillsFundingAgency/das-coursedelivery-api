using System.Collections.Generic;
using System.Threading.Tasks;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.CourseDelivery.Domain.Interfaces;

namespace SFA.DAS.CourseDelivery.Data.Repository
{
    public class NationalAchievementRateRepository : INationalAchievementRateRepository
    {
        private readonly ICourseDeliveryDataContext _dataContext;

        public NationalAchievementRateRepository (ICourseDeliveryDataContext dataContext)
        {
            _dataContext = dataContext;
        }
        
        public void DeleteAll()
        {
            _dataContext.NationalAchievementRates.RemoveRange(_dataContext.NationalAchievementRates);
            _dataContext.SaveChanges();
        }

        public async Task InsertMany(IEnumerable<NationalAchievementRate> items)
        {
            await _dataContext.NationalAchievementRates.AddRangeAsync(items);
            _dataContext.SaveChanges();
        }
    }
}