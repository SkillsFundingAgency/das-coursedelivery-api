using SFA.DAS.CourseDelivery.Domain.Entities;

namespace SFA.DAS.CourseDelivery.Domain.Models
{
    public class AchievementRate
    {
        public int Ukprn { get ; set ; }
        public string SectorSubjectArea { get ; set ; }

        public decimal? OverallAchievementRate { get ; set ; }

        public int? OverallCohort { get ; set ; }

        public ApprenticeshipLevel ApprenticeshipLevel { get ; set ; }

        public Age Age { get ; set ; }


        public static implicit operator AchievementRate(ProviderWithStandardAndLocation source)
        {
            if (!source.Age.HasValue)
            {
                return null;
            }
            
            return new AchievementRate
            {
                Ukprn = source.Ukprn,
                Age = source.Age.Value,
                ApprenticeshipLevel = source.ApprenticeshipLevel.Value,
                OverallCohort = source.OverallCohort,
                OverallAchievementRate = source.OverallAchievementRate,
                SectorSubjectArea = source.SectorSubjectArea
                
            };
        }

        public static implicit operator AchievementRate(NationalAchievementRate source)
        {
            return new AchievementRate
            {
                Ukprn = source.Ukprn,
                Age = source.Age,
                ApprenticeshipLevel = source.ApprenticeshipLevel,
                OverallCohort = source.OverallCohort,
                OverallAchievementRate = source.OverallAchievementRate,
                SectorSubjectArea = source.SectorSubjectArea
            };
        }
    }
}