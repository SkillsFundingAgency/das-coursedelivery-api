using CsvHelper.Configuration.Attributes;

namespace SFA.DAS.CourseDelivery.Domain.ImportTypes
{
    public class NationalAchievementRateOverallCsv
    {
        [Name("Age_Band")]
        public string Age { get; set; }
        [Name("Sector_Subject_Area_T2")]
        public string SectorSubjectArea { get; set; }
        [Name("Apprenticeship_Level_Grouped")]
        public string ApprenticeshipLevel { get; set; }
        [Name("Overall_Leavers")]
        public string OverallCohort { get; set; }
        [Name("Overall_Achievement_Rate")]
        public string OverallAchievementRate { get; set; }
    }
}