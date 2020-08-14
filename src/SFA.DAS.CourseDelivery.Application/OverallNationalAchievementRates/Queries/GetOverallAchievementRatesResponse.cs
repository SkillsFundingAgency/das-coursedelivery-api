using System.Collections.Generic;
using SFA.DAS.CourseDelivery.Domain.Entities;

namespace SFA.DAS.CourseDelivery.Application.OverallNationalAchievementRates.Queries
{
    public class GetOverallAchievementRatesResponse
    {
        public IEnumerable<NationalAchievementRateOverall> OverallAchievementRates { get ; set ; }
    }
}