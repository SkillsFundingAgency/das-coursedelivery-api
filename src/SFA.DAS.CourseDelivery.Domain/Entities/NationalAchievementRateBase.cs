namespace SFA.DAS.CourseDelivery.Domain.Entities
{
    public class NationalAchievementRateBase
    {
        public long Id { get; set; }
        public int Ukprn { get; set; }
        public Age Age { get; set; }
        public string SectorSubjectArea { get; set; }
        public ApprenticeshipLevel ApprenticeshipLevel { get; set; }
        public int? OverallCohort { get; set; }
        public decimal? OverallAchievementRate { get; set; }
        
        
    }
    public enum ApprenticeshipLevel : short
    {
        Unknown = 0,
        AllLevels = 1,
        Two = 2,
        Three = 3,
        FourPlus = 4
    }

    public enum Age : short
    {
        Unknown = 0,
        SixteenToEighteen = 1,
        NineteenToTwentyThree = 2,
        TwentyFourPlus = 3,
        AllAges = 4
    }
}