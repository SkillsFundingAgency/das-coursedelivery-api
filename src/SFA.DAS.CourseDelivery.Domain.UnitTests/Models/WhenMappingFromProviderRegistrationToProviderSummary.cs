using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Domain.Models;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CourseDelivery.Domain.UnitTests.Models
{
    public class WhenMappingFromProviderRegistrationToProviderSummary
    {
        [Test, RecursiveMoqAutoData]
        public void Then_The_Fields_Are_Correctly_Mapped_With_No_Provider(Domain.Entities.ProviderRegistration source)
        {
            //Arrange
            source.Provider = null;
            
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
        
        [Test, RecursiveMoqAutoData]
        public void Then_The_Fields_Are_Correctly_Mapped_With_Provider(Domain.Entities.Provider source)
        {
            //Act
            var actual = (ProviderSummary) source;
            
            //Assert
            actual.Name.Should().Be(source.Name);
            actual.TradingName.Should().Be(source.TradingName);
            actual.Ukprn.Should().Be(source.Ukprn);
            actual.Email.Should().Be(source.Email);
            actual.Phone.Should().Be(source.Phone);
            actual.ContactUrl.Should().Be(source.Website);
            actual.Address.Should().BeEquivalentTo((ProviderSummaryAddress)source.ProviderRegistration);
        }

        [Test, RecursiveMoqAutoData]
        public void Then_Returns_Null_If_Source_Is_Null()
        {
            //Act
            var actual = (ProviderSummary) (Domain.Entities.ProviderRegistration)null;
            
            //Assert
            actual.Should().BeNull();
        }
    }
}