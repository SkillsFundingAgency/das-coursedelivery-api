using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Api.ApiResponses;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CourseDelivery.Api.UnitTests.ApiResponses
{
    public class WhenMappingGetOverallAchievementRateResponseFromNationalAchievementRateOverall
    {
        [Test, RecursiveMoqAutoData]
        public void Then_Maps_Fields(NationalAchievementRateOverall source)
        {
            var actual = (GetOverallAchievementRateResponse) source;
            
            actual.Should().BeEquivalentTo(source, options=>options
                .Excluding(c=>c.Id)
                .Excluding(c=>c.Age)
                .Excluding(c=>c.ApprenticeshipLevel)
            );
            actual.ApprenticeshipLevel.Should().Be(source.ApprenticeshipLevel.ToString());
            actual.Age.Should().Be(source.Age.ToString());
        }
    }
}