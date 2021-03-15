using System.Collections.Generic;
using System.Linq;
using AutoFixture.NUnit3;
using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Data.Extensions;
using SFA.DAS.CourseDelivery.Domain.ImportTypes;
using SFA.DAS.Testing.AutoFixture;
using Provider = SFA.DAS.CourseDelivery.Domain.Entities.Provider;

namespace SFA.DAS.CourseDelivery.Data.UnitTests.Extensions
{
    public class WhenFilteringRegisteredProviders
    {
        [Test, RecursiveMoqAutoData]
        public void And_Not_Active_Then_Filtered(
            List<Provider> providers)
        {
            foreach (var provider in providers)
            {
                provider.ProviderRegistration.StatusId = RoatpTypeConstants.StatusOfActive;
                provider.ProviderRegistration.ProviderTypeId = RoatpTypeConstants.ProviderTypeOfMainProvider;
            }
            providers[0].ProviderRegistration.StatusId = RoatpTypeConstants.StatusOfActive + 1;
            var expectedProviders = providers.Where(provider => provider.Ukprn != providers[0].Ukprn);

            var filtered = providers.AsQueryable().FilterRegisteredProviders();

            filtered.Should().BeEquivalentTo(expectedProviders);
        }

        [Test, RecursiveMoqAutoData]
        public void And_Not_MainProvider_Then_Filtered(
            List<Provider> providers)
        {
            foreach (var provider in providers)
            {
                provider.ProviderRegistration.StatusId = RoatpTypeConstants.StatusOfActive;
                provider.ProviderRegistration.ProviderTypeId = RoatpTypeConstants.ProviderTypeOfMainProvider;
            }
            providers[0].ProviderRegistration.ProviderTypeId = RoatpTypeConstants.ProviderTypeOfMainProvider + 1;
            var expectedProviders = providers.Where(provider => provider.Ukprn != providers[0].Ukprn);

            var filtered = providers.AsQueryable().FilterRegisteredProviders();

            filtered.Should().BeEquivalentTo(expectedProviders);
        }
    }
}