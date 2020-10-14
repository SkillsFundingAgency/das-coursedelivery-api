using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.CourseDelivery.Domain.Interfaces;

namespace SFA.DAS.CourseDelivery.Data.Repository
{
    public class NationalAchievementRateOverallRepository : INationalAchievementRateOverallRepository
    {
        private readonly ICourseDeliveryDataContext _dataContext;
        private readonly ICourseDeliveryReadonlyDataContext _readonlyDataContext;

        public NationalAchievementRateOverallRepository (ICourseDeliveryDataContext dataContext, ICourseDeliveryReadonlyDataContext readonlyDataContext)
        {
            _dataContext = dataContext;
            _readonlyDataContext = readonlyDataContext;
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

        public async Task<IEnumerable<NationalAchievementRateOverall>> GetBySectorSubjectArea(string expectedSectorSubjectArea)
        {
            var results = await _readonlyDataContext.NationalAchievementRateOverall.Where(c =>
                    c.SectorSubjectArea.Equals(expectedSectorSubjectArea))
                .ToListAsync();

            return results;
        }
    }
}