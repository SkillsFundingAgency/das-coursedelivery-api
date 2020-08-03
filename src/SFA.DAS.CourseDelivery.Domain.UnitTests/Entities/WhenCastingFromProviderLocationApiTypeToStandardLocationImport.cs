using AutoFixture.NUnit3;
using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.CourseDelivery.Domain.ImportTypes;

namespace SFA.DAS.CourseDelivery.Domain.UnitTests.Entities
{
    public class WhenCastingFromProviderCourseLocationApiTypeToStandardLocationImport
    {
        [Test, AutoData]
        public void Then_Maps_The_Fields(CourseLocation providerCourseLocation)
        {
            var actual = (StandardLocationImport) providerCourseLocation;
            
            actual.Should().BeEquivalentTo(providerCourseLocation, options => options.Excluding(c=>c.Address).Excluding(c=>c.Id));
            actual.Should().BeEquivalentTo(providerCourseLocation.Address);
            actual.LocationId.Should().Be(providerCourseLocation.Id);
        }
    }
}