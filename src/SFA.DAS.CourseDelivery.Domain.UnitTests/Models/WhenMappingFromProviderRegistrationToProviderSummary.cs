using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Domain.Models;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CourseDelivery.Domain.UnitTests.Models
{
    public class WhenMappingFromProviderRegistrationToProviderSummary
    {
        [Test, RecursiveMoqAutoData]
        public void Then_The_Fields_Are_Correctly_Mapped(Domain.Entities.ProviderRegistration source)
        {
            //Act
            var actual = (ProviderSummary) source;
            
            //Assert
            actual.Name.Should().Be(source.LegalName);
            actual.TradingName.Should().Be(source.LegalName);
            actual.Ukprn.Should().Be(source.Ukprn);
            actual.Email.Should().BeEmpty();
            actual.Phone.Should().BeEmpty();
            actual.ContactUrl.Should().BeEmpty();
            actual.Address.Should().BeEquivalentTo((ProviderSummaryAddress)source);
        }
    }
}