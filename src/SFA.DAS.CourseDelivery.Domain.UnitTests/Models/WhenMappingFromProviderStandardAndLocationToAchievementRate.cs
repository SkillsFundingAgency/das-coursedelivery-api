using AutoFixture.NUnit3;
using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.CourseDelivery.Domain.Models;

namespace SFA.DAS.CourseDelivery.Domain.UnitTests.Models
{
    public class WhenMappingFromProviderStandardAndLocationToAchievementRate
    {
        [Test, AutoData]
        public void Then_The_Fields_Are_Correctly_Mapped(ProviderWithStandardAndLocation source)
        {
            var actual = (AchievementRate) source;
            
            actual.Should().BeEquivalentTo(source, options=> options
                .Excluding(c=>c.Id)
                .Excluding(c=>c.Name)
                .Excluding(c=>c.LocationId)
                .Excluding(c=>c.DeliveryModes)
                .Excluding(c=>c.DistanceInMiles)
            );
        }
    }
}