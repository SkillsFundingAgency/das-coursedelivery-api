using System.Collections.Generic;
using SFA.DAS.CourseDelivery.Domain.Entities;

namespace SFA.DAS.CourseDelivery.Application.Provider.Queries.Provider
{
    public class GetProviderResponse
    {
        public ProviderStandard ProviderStandardContact { get ; set ; }
        public IEnumerable<NationalAchievementRateOverall> OverallAchievementRates { get ; set ; }
    }
}