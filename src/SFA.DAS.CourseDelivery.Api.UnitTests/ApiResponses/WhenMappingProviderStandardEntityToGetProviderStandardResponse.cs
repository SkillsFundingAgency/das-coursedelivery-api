using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Api.ApiResponses;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CourseDelivery.Api.UnitTests.ApiResponses
{
    public class WhenMappingProviderStandardEntityToGetProviderStandardResponse
    {
        [Test, RecursiveMoqAutoData]
        public void Then_Maps_Fields(ProviderStandard provider)
        {
            var actual = (GetProviderStandardResponse) provider;

            actual.Email.Should().Be(provider.Email);
            actual.ContactUrl.Should().Be(provider.ContactUrl);
            actual.Phone.Should().Be(provider.Phone);
            actual.StandardId.Should().Be(provider.StandardId);
        }
    }
}