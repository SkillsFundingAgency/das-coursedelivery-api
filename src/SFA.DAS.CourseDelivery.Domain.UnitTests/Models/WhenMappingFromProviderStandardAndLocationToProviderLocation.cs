using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.CourseDelivery.Domain.Models;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CourseDelivery.Domain.UnitTests.Models
{
    public class WhenMappingFromProviderStandardAndLocationToProviderLocation
    {
        [Test, RecursiveMoqAutoData]
        public void Then_The_Fields_Are_Correctly_Mapped(Provider source)
        {
            var actual = new ProviderLocation(source);

            actual.Name.Should().Be(source.Name);
            actual.Ukprn.Should().Be(source.Ukprn);
            actual.AchievementRates.Should().NotBeEmpty();
            actual.DeliveryTypes.Should().BeEmpty();
        }
    }
}