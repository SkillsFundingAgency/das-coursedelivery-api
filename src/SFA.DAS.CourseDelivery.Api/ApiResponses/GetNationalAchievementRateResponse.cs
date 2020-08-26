using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.CourseDelivery.Domain.Models;

namespace SFA.DAS.CourseDelivery.Api.ApiResponses
{
    public class GetNationalAchievementRateResponse
    {
        public string Age { get ; set ; }

        public string Level { get ; set ; }

        public string SectorSubjectArea { get ; set ; }

        public decimal? OverallAchievementRate { get ; set ; }

        public int? OverallCohort { get ; set ; }

        public int Ukprn { get ; set ; }

        public static implicit operator GetNationalAchievementRateResponse(AchievementRate source)
        {
            return new GetNationalAchievementRateResponse
            {
                Ukprn = source.Ukprn,
                OverallCohort = source.OverallCohort,
                OverallAchievementRate = source.OverallAchievementRate,
                SectorSubjectArea = source.SectorSubjectArea,
                Level = source.ApprenticeshipLevel.ToString(),
                Age = source.Age.ToString()
            };
        }
        public static implicit operator GetNationalAchievementRateResponse(NationalAchievementRate source)
        {
            return new GetNationalAchievementRateResponse
            {
                Ukprn = source.Ukprn,
                OverallCohort = source.OverallCohort,
                OverallAchievementRate = source.OverallAchievementRate,
                SectorSubjectArea = source.SectorSubjectArea,
                Level = source.ApprenticeshipLevel.ToString(),
                Age = source.Age.ToString()
            };
        }
    }
}