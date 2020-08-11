using System.Collections.Generic;
using System.Threading.Tasks;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.CourseDelivery.Domain.Interfaces;

namespace SFA.DAS.CourseDelivery.Data.Repository
{
    public class NationalAchievementRateOverallRepository : INationalAchievementRateOverallRepository
    {
        private readonly ICourseDeliveryDataContext _dataContext;

        public NationalAchievementRateOverallRepository (ICourseDeliveryDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void DeleteAll()
        {
            _dataContext.NationalAchievementRateOverall.RemoveRange(_dataContext.NationalAchievementRateOverall);
            _dataContext.SaveChanges();
        }

        public async Task InsertMany(IEnumerable<NationalAchievementRateOverall> items)
        {
            await _dataContext.NationalAchievementRateOverall.AddRangeAsync(items);
            _dataContext.SaveChanges();
        }
    }
}