using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.CourseDelivery.Domain.Models;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CourseDelivery.Domain.UnitTests.Models
{
    public class WhenMappingFromProviderRegistrationToProviderSummaryAddress
    {
        [Test, RecursiveMoqAutoData]
        public void Then_The_Fields_Are_Correctly_Mapped(ProviderRegistration source)
        {
            var actual = (ProviderSummaryAddress)source;

            actual.Should().BeEquivalentTo(source, options => options.ExcludingMissingMembers());
        }

        [Test]
        public void And_Null_Then_Returns_Null()
        {
            var actual = (ProviderSummaryAddress)null;

            actual.Should().BeNull();
        }
    }
}