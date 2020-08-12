using System.Collections.Generic;
using System.Threading.Tasks;
using SFA.DAS.CourseDelivery.Domain.Entities;

namespace SFA.DAS.CourseDelivery.Domain.Interfaces
{
    public interface INationalAchievementRateRepository
    {
        void DeleteAll();
        Task InsertMany(IEnumerable<NationalAchievementRate> items);
    }
}