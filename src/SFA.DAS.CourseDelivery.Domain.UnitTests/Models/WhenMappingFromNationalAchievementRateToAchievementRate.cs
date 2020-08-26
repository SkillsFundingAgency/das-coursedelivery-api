using AutoFixture.NUnit3;
using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.CourseDelivery.Domain.Models;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CourseDelivery.Domain.UnitTests.Models
{
    public class WhenMappingFromNationalAchievementRateToAchievementRate
    {
        [Test, RecursiveMoqAutoData]
        public void Then_The_Fields_Are_Correctly_Mapped(NationalAchievementRate source)
        {
            var actual = (AchievementRate) source;
            
            actual.Should().BeEquivalentTo(source, options=>options
                .Excluding(c=>c.Provider)
                .Excluding(c=>c.ProviderStandard)
                .Excluding(c=>c.Id)
            );
        }
    }
}