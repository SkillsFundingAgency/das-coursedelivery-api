using AutoFixture.NUnit3;
using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Domain.Entities;

namespace SFA.DAS.CourseDelivery.Domain.UnitTests.Entities
{
    public class WhenCastingFromNationalAchievementRateImportToNationalAchievementRate
    {
        [Test, AutoData]
        public void Then_The_Fields_Are_Correctly_Mapped(NationalAchievementRateImport source)
        {
            var actual = (NationalAchievementRate) source;
            
            actual.Should().BeEquivalentTo(source, options=>options.Excluding(c=>c.Id));
        }
    }
}