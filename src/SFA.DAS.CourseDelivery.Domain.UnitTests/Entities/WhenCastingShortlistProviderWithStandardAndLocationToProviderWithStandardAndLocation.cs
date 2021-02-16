using AutoFixture.NUnit3;
using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Domain.Entities;

namespace SFA.DAS.CourseDelivery.Domain.UnitTests.Entities
{
    public class WhenCastingShortlistProviderWithStandardAndLocationToProviderWithStandardAndLocation
    {
        [Test, AutoData]
        public void Then_The_Fields_Are_Correctly_Mapped(ShortlistProviderWithStandardAndLocation source)
        {
            //Act
            var actual = (ProviderWithStandardAndLocation) source;
            
            //Assert
            actual.Should().BeEquivalentTo(source, options=>options
                .Excluding(c=>c.ShortlistUserId)
                .Excluding(c=>c.LocationDescription)
                .Excluding(c=>c.StandardId)
                .Excluding(c=>c.CreatedDate)
            );
        }
    }
}