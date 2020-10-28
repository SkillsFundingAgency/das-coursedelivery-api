using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.CourseDelivery.Domain.Models;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CourseDelivery.Domain.UnitTests.Models
{
    public class WhenMappingFromProviderStandardToProviderLocation
    {
        [Test, RecursiveMoqAutoData]
        public void Then_The_Fields_Are_Correctly_Mapped(ProviderStandard source)
        {
            var actual = (ProviderLocation) source;

            actual.Email.Should().Be(source.Email);
            actual.Phone.Should().Be(source.Phone);
            actual.ContactUrl.Should().Be(source.ContactUrl);
            actual.Name.Should().Be(source.Provider.Name);
            actual.TradingName.Should().Be(source.Provider.TradingName);
            actual.Ukprn.Should().Be(source.Provider.Ukprn);
            actual.AchievementRates.Should().NotBeEmpty();
            actual.DeliveryTypes.Should().BeEmpty();
            actual.FeedbackRating.Should().NotBeEmpty();
            actual.FeedbackAttributes.Should().NotBeEmpty();
        }
        
    }
}