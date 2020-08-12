using SFA.DAS.CourseDelivery.Domain.Entities;

namespace SFA.DAS.CourseDelivery.Api.ApiResponses
{
    public class GetOverallAchievementRateResponse
    {
        public string Age { get ; set ; }

        public string ApprenticeshipLevel { get ; set ; }

        public string SectorSubjectArea { get ; set ; }

        public decimal? OverallAchievementRate { get ; set ; }

        public int? OverallCohort { get ; set ; }

        public static implicit operator GetOverallAchievementRateResponse(NationalAchievementRateOverall source)
        {
            return new GetOverallAchievementRateResponse
            {
                OverallCohort = source.OverallCohort,
                OverallAchievementRate = source.OverallAchievementRate,
                SectorSubjectArea = source.SectorSubjectArea,
                Age = source.Age.ToString(),
                ApprenticeshipLevel = source.ApprenticeshipLevel.ToString()
            };
        }
    }
}