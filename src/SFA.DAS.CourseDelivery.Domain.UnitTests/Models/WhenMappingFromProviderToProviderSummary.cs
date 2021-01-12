using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.CourseDelivery.Domain.Models;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CourseDelivery.Domain.UnitTests.Models
{
    public class WhenMappingFromProviderToProviderSummary
    {
        [Test, RecursiveMoqAutoData]
        public void Then_The_Fields_Are_Correctly_Mapped(Provider source)
        {
            var actual = (ProviderSummary)source;

            actual.Name.Should().Be(source.Name);
            actual.Ukprn.Should().Be(source.Ukprn);
            actual.Email.Should().Be(source.Email);
            actual.Phone.Should().Be(source.Phone);
            actual.ContactUrl.Should().Be(source.Website);
            actual.TradingName.Should().Be(source.TradingName);
            actual.Address.Should().BeEquivalentTo(source.ProviderRegistration, options => options.ExcludingMissingMembers());
        }

        [Test]
        public void And_Null_Then_Returns_Null()
        {
            var actual = (ProviderSummary)null;

            actual.Should().BeNull();
        }
    }
}