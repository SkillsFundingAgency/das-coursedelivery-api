namespace SFA.DAS.CourseDelivery.Domain.Entities
{
    public class NationalAchievementRateOverallBase
    {
        public long Id { get; set; }
        public Age Age { get; set; }
        public string SectorSubjectArea { get; set; }
        public ApprenticeshipLevel ApprenticeshipLevel { get; set; }
        public int? OverallCohort { get; set; }
        public decimal? OverallAchievementRate { get; set; }
    }
}