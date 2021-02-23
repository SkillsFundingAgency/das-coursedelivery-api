using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Api.ApiResponses;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.CourseDelivery.Domain.Models;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CourseDelivery.Api.UnitTests.ApiResponses
{
    public class WhenMappingGetProviderDetailsResponseFromMediatorType
    {
        [Test, RecursiveMoqAutoData]
        public void Then_Maps_Fields(ProviderSummary source)
        {
            var result = (GetProviderDetailsResponse) source;

            result.Should().BeEquivalentTo(source, options => options
                .Excluding(c=>c.ContactUrl)
                .Excluding(c=>c.Address)
            );
            result.Website.Should().Be(source.ContactUrl);
        }

        [Test]
        public void And_Provider_Null_Then_Returns_Default()
        {
            var result = (GetProviderDetailsResponse) null;

            result.Should().Be(default);
        }
    }
}