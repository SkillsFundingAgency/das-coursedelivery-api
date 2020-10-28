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
        public void Then_The_Fields_Are_Correctly_Mapped(string name, int ukprn, string contactUrl,
            string email, 
            string phone, 
            string tradingName,
            double providerDistanceInMiles, 
            string providerHeadOfficeAddress1,
            string providerHeadOfficeAddress2,
            string providerHeadOfficeAddress3,
            string providerHeadOfficeAddress4,
            string providerHeadOfficeAddressTown,
            string providerHeadOfficeAddressPostcode,
            List<ProviderWithStandardAndLocation> providerWithStandardAndLocations)
        {
            var actual = new ProviderLocation(ukprn, name, tradingName, contactUrl, phone, email, 
                providerDistanceInMiles,
                providerHeadOfficeAddress1,
                providerHeadOfficeAddress2, 
                providerHeadOfficeAddress3, 
                providerHeadOfficeAddress4, 
                providerHeadOfficeAddressTown, 
                providerHeadOfficeAddressPostcode, providerWithStandardAndLocations);

            actual.Name.Should().Be(name);
            actual.TradingName.Should().Be(tradingName);
            actual.Ukprn.Should().Be(ukprn);
            actual.ContactUrl.Should().Be(contactUrl);
            actual.Email.Should().Be(email);
            actual.Phone.Should().Be(phone);
            actual.AchievementRates.Should().NotBeEmpty();
            actual.DeliveryTypes.Should().NotBeEmpty();
            actual.Address.Address1.Should().Be(providerHeadOfficeAddress1);
            actual.Address.Address2.Should().Be(providerHeadOfficeAddress2);
            actual.Address.Address3.Should().Be(providerHeadOfficeAddress3);
            actual.Address.Address4.Should().Be(providerHeadOfficeAddress4);
            actual.Address.Town.Should().Be(providerHeadOfficeAddressTown);
            actual.Address.Postcode.Should().Be(providerHeadOfficeAddressPostcode);
            actual.Address.DistanceInMiles.Should().Be(providerDistanceInMiles);
        }
        
    }
}