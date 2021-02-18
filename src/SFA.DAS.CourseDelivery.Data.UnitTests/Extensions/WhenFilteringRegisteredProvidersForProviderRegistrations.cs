using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Data.Extensions;
using SFA.DAS.CourseDelivery.Domain.ImportTypes;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CourseDelivery.Data.UnitTests.Extensions
{
    public class WhenFilteringRegisteredProvidersForProviderRegistrations
    {
        [Test, RecursiveMoqAutoData]
        public void And_Not_Active_Then_Filtered(
            List<ProviderRegistration> providers)
        {
            foreach (var provider in providers)
            {
                provider.StatusId = RoatpTypeConstants.StatusOfActive;
                provider.ProviderTypeId = RoatpTypeConstants.ProviderTypeOfMainProvider;
            }
            providers[0].StatusId = RoatpTypeConstants.StatusOfActive + 1;
            var expectedProviders = providers.Where(provider => provider.Ukprn != providers[0].Ukprn);

            var filtered = providers.AsQueryable().FilterRegisteredProviders();

            filtered.Should().BeEquivalentTo(expectedProviders);
        }

        [Test, RecursiveMoqAutoData]
        public void And_Not_MainProvider_Then_Filtered(
            List<ProviderRegistration> providers)
        {
            foreach (var provider in providers)
            {
                provider.StatusId = RoatpTypeConstants.StatusOfActive;
                provider.ProviderTypeId = RoatpTypeConstants.ProviderTypeOfMainProvider;
            }
            providers[0].ProviderTypeId = RoatpTypeConstants.ProviderTypeOfMainProvider + 1;
            var expectedProviders = providers.Where(provider => provider.Ukprn != providers[0].Ukprn);

            var filtered = providers.AsQueryable().FilterRegisteredProviders();

            filtered.Should().BeEquivalentTo(expectedProviders);
        }
    }
}