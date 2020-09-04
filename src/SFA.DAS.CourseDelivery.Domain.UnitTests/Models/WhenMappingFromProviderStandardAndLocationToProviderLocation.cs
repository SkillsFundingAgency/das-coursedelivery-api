using System.Collections.Generic;
using AutoFixture.NUnit3;
using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.CourseDelivery.Domain.Models;

namespace SFA.DAS.CourseDelivery.Domain.UnitTests.Models
{
    public class WhenMappingFromProviderStandardAndLocationToProviderLocation
    {
        [Test, AutoData]
        public void Then_The_Fields_Are_Correctly_Mapped(string name, int ukprn, string contactUrl, string email, string phone, List<ProviderWithStandardAndLocation> providerWithStandardAndLocations)
        {
            var actual = new ProviderLocation(ukprn, name,contactUrl, phone, email, providerWithStandardAndLocations);

            actual.Name.Should().Be(name);
            actual.Ukprn.Should().Be(ukprn);
            actual.ContactUrl.Should().Be(contactUrl);
            actual.Email.Should().Be(email);
            actual.Phone.Should().Be(phone);
            actual.AchievementRates.Should().NotBeEmpty();
            actual.DeliveryTypes.Should().NotBeEmpty();
        }
        
    }
}