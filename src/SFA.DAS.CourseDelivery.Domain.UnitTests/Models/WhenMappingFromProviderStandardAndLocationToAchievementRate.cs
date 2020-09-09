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
                .Excluding(c=>c.ContactUrl)
                .Excluding(c=>c.Email)
                .Excluding(c=>c.Phone)
                .Excluding(c=>c.Address1)
                .Excluding(c=>c.Address2)
                .Excluding(c=>c.Town)
                .Excluding(c=>c.Postcode)
                .Excluding(c=>c.County)
            );
        }
    }
}