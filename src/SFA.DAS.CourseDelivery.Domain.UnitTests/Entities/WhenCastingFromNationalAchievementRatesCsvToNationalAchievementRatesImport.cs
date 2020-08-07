using AutoFixture.NUnit3;
using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.CourseDelivery.Domain.ImportTypes;

namespace SFA.DAS.CourseDelivery.Domain.UnitTests.Entities
{
    public class WhenCastingFromNationalAchievementRatesCsvToNationalAchievementRatesImport
    {
        [Test, AutoData]
        public void Then_The_Fields_Are_Correctly_Mapped(
            int ukprn, 
            string sectorSubjectArea, 
            int overallCohort, 
            int overallAchievementRate)
        {
            //Arrange
            var source = new NationalAchievementRateCsv
            {
                Ukprn = ukprn,
                SectorSubjectArea = sectorSubjectArea,
                OverallCohort = overallCohort.ToString(),
                OverallAchievementRate = overallAchievementRate.ToString(),
                Age = "16-18",
                ApprenticeshipLevel = "4+"
            };
            
            //Act
            var actual = (NationalAchievementRateImport) source;

            //Assert
            actual.Ukprn.Should().Be(ukprn);
            actual.SectorSubjectArea.Should().Be(sectorSubjectArea);
            actual.OverallCohort.Should().Be(overallCohort);
            actual.OverallAchievementRate.Should().Be(overallAchievementRate);
            actual.ApprenticeshipLevel.Should().Be(ApprenticeshipLevel.FourPlus);
            actual.Age.Should().Be(Age.SixteenToEighteen);
        }

        [TestCase("16-18", Age.SixteenToEighteen)]
        [TestCase("19-23", Age.NineteenToTwentyThree)]
        [TestCase("24+", Age.TwentyFourPlus)]
        [TestCase("All age", Age.AllAges)]
        [TestCase("test", Age.Unknown)]
        public void Then_The_Age_Enum_Is_Correctly_Mapped(string source, Age expected)
        {
            //Arrange
            var csvSource = new NationalAchievementRateCsv
            {
                Ukprn = 1234,
                SectorSubjectArea = "test",
                OverallCohort = "0",
                OverallAchievementRate = "0",
                Age = source,
                ApprenticeshipLevel = "4+"
            };
            
            //Act
            var actual = (NationalAchievementRateImport) csvSource;
            
            //Assert
            actual.Age.Should().Be(expected);
        }
        [TestCase("2", ApprenticeshipLevel.Two)]
        [TestCase("3", ApprenticeshipLevel.Three)]
        [TestCase("4+", ApprenticeshipLevel.FourPlus)]
        [TestCase("All levels", ApprenticeshipLevel.AllLevels)]
        [TestCase("test", ApprenticeshipLevel.Unknown)]
        public void Then_The_Level_Enum_Is_Correctly_Mapped(string source, Age expected)
        {
            //Arrange
            var csvSource = new NationalAchievementRateCsv
            {
                Ukprn = 1234,
                SectorSubjectArea = "test",
                OverallCohort = "0",
                OverallAchievementRate = "0",
                Age = "",
                ApprenticeshipLevel = source
            };
            
            //Act
            var actual = (NationalAchievementRateImport) csvSource;
            
            //Assert
            actual.ApprenticeshipLevel.Should().Be(expected);
        }
        

        [Test, AutoData]
        public void Then_If_The_Overall_Values_Are_Not_Numeric_Null_Is_Stored(int ukprn, 
            string sectorSubjectArea, 
            int overallCohort, 
            int overallAchievementRate)
        {
            //Arrange
            var source = new NationalAchievementRateCsv
            {
                Ukprn = ukprn,
                SectorSubjectArea = sectorSubjectArea,
                OverallCohort = "-",
                OverallAchievementRate = "*",
                Age = "16-18",
                ApprenticeshipLevel = "4+"
            };
            
            //Act
            var actual = (NationalAchievementRateImport) source;
            
            //Assert
            actual.OverallCohort.Should().BeNull();
            actual.OverallAchievementRate.Should().BeNull();
        }
    }
}