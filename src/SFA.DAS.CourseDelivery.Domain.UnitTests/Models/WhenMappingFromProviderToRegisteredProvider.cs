using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Domain.Models;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CourseDelivery.Domain.UnitTests.Models
{
    public class WhenMappingFromProviderToRegisteredProvider
    {
        [Test, RecursiveMoqAutoData]
        public void Then_The_Fields_Are_Correctly_Mapped(Domain.Entities.Provider source)
        {
            //Act
            var actual = (RegisteredProvider) source;
            
            //Assert
            actual.Should().BeEquivalentTo(source, options=> options.ExcludingMissingMembers());
        }
    }
    public class WhenMappingFromProviderRegistrationToRegisteredProvider
    {
        [Test, RecursiveMoqAutoData]
        public void Then_The_Fields_Are_Correctly_Mapped(Domain.Entities.ProviderRegistration source)
        {
            //Act
            var actual = (RegisteredProvider) source;
            
            //Assert
            actual.Name.Should().Be(source.LegalName);
            actual.TradingName.Should().Be(source.LegalName);
            actual.Ukprn.Should().Be(source.Ukprn);
        }
    }
}