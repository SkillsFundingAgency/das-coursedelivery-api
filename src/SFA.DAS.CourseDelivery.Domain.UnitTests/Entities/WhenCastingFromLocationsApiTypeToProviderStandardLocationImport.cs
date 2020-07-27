using System.Linq;
using AutoFixture.NUnit3;
using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.CourseDelivery.Domain.ImportTypes;

namespace SFA.DAS.CourseDelivery.Domain.UnitTests.Entities
{
    public class WhenCastingFromLocationsApiTypeToProviderStandardLocationImport
    {
        [Test, AutoData]
        public void Then_Maps_The_Fields(ImportTypes.Provider provider)
        {
            //Arrange
            var courseStandard = provider.Standards.FirstOrDefault();
            var courseLocation = courseStandard.Locations.FirstOrDefault();
            var actual = new ProviderStandardLocationImport();
            
            //Act
            actual = actual.Map(courseLocation, provider.Ukprn, courseStandard.StandardCode );
            
            //Assert
            actual.Ukprn.Should().Be(provider.Ukprn);
            actual.StandardId.Should().Be(courseStandard.StandardCode);
            actual.Radius.Should().Be(courseLocation.Radius);
            actual.DeliveryModes.Should().Be(string.Join("|",courseLocation.DeliveryModes));
        }
    }
}