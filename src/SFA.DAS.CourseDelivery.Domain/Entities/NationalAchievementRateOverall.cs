namespace SFA.DAS.CourseDelivery.Domain.Entities
{
    public class NationalAchievementRateOverall : NationalAchievementRateOverallBase
    {
        public static implicit operator NationalAchievementRateOverall(NationalAchievementRateOverallImport source)
        {
            return new NationalAchievementRateOverall
            {
                Age = source.Age,
                ApprenticeshipLevel = source.ApprenticeshipLevel,
                OverallCohort = source.OverallCohort,
                OverallAchievementRate = source.OverallAchievementRate,
                SectorSubjectArea = source.SectorSubjectArea
            };
        }
    }
}