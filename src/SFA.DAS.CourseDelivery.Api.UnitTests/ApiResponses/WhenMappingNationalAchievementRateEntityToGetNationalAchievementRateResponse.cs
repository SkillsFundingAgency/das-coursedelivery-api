using AutoFixture.NUnit3;
using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Api.ApiResponses;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CourseDelivery.Api.UnitTests.ApiResponses
{
    public class WhenMappingNationalAchievementRateEntityToGetNationalAchievementRateResponse
    {
        [Test, RecursiveMoqAutoData]
        public void Then_The_Fields_Are_Correctly_Mapped(NationalAchievementRate source)
        {
            var actual = (GetNationalAchievementRateResponse) source;
            
            actual.Ukprn.Should().Be(source.Ukprn);
            actual.Age.Should().Be(source.Age.ToString());
            actual.Level.Should().Be(source.ApprenticeshipLevel.ToString());
            actual.OverallCohort.Should().Be(source.OverallCohort);
            actual.OverallAchievementRate.Should().Be(source.OverallAchievementRate);
            actual.SectorSubjectArea.Should().Be(source.SectorSubjectArea);
        }
    }
}