using AutoFixture.NUnit3;
using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.CourseDelivery.Domain.ImportTypes;

namespace SFA.DAS.CourseDelivery.Domain.UnitTests.Entities
{
    public class WhenCastingFromNationalAchievementRatesOverallCsvToNationalAchievementRatesOverallImport
    {
        [Test, AutoData]
        public void Then_The_Fields_Are_Correctly_Mapped(
            int ukprn, 
            string sectorSubjectArea, 
            int overallCohort, 
            int overallAchievementRate)
        {
            //Arrange
            var source = new NationalAchievementRateOverallCsv
            {
                SectorSubjectArea = sectorSubjectArea,
                OverallCohort = overallCohort.ToString(),
                OverallAchievementRate = overallAchievementRate.ToString(),
                Age = "16-18",
                ApprenticeshipLevel = "4+"
            };
            
            //Act
            var actual = (NationalAchievementRateOverallImport) source;

            //Assert
            actual.SectorSubjectArea.Should().Be(sectorSubjectArea);
            actual.OverallCohort.Should().Be(overallCohort);
            actual.OverallAchievementRate.Should().Be(overallAchievementRate);
            actual.ApprenticeshipLevel.Should().Be(ApprenticeshipLevel.FourPlus);
            actual.Age.Should().Be(Age.SixteenToEighteen);
        }
        
        
        [Test, AutoData]
        public void Then_If_The_Overall_Values_Are_Not_Numeric_Null_Is_Stored(int ukprn, 
            string sectorSubjectArea, 
            int overallCohort, 
            int overallAchievementRate)
        {
            //Arrange
            var source = new NationalAchievementRateOverallCsv
            {
                SectorSubjectArea = sectorSubjectArea,
                OverallCohort = "*",
                OverallAchievementRate = "-",
                Age = "16-18",
                ApprenticeshipLevel = "4+"
            };
            
            //Act
            var actual = (NationalAchievementRateOverallImport) source;
            
            //Assert
            actual.OverallCohort.Should().BeNull();
            actual.OverallAchievementRate.Should().BeNull();
        }
    }
}