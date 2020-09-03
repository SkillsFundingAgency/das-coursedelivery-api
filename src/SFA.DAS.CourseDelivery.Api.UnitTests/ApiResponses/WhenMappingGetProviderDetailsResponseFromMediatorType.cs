using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Api.ApiResponses;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CourseDelivery.Api.UnitTests.ApiResponses
{
    public class WhenMappingGetProviderDetailsResponseFromMediatorType
    {
        [Test, RecursiveMoqAutoData]
        public void Then_Maps_Fields(Provider source)
        {
            var result = (GetProviderDetailsResponse) source;

            result.Should().BeEquivalentTo(source, options => options
                .Excluding(provider => provider.ProviderStandards)
                .Excluding(provider => provider.NationalAchievementRates)
                .Excluding(provider => provider.ProviderRegistration));
        }

        [Test]
        public void And_Provider_Null_Then_Returns_Default()
        {
            var result = (GetProviderDetailsResponse) null;

            result.Should().Be(default);
        }
    }
}