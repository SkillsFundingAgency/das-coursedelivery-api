using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Api.ApiResponses;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CourseDelivery.Api.UnitTests.ApiResponses
{
    public class WhenMappingGetCourseProviderResponseFromMediatorType
    {
        [Test, RecursiveMoqAutoData]
        public void Then_Maps_Fields(Application.Provider.Queries.ProviderByCourse.GetCourseProviderQueryResponse source)
        {
            var actual = GetProviderDetailResponse.Map(source.ProviderStandardLocation);

            actual.Name.Should().Be(source.ProviderStandardLocation.Name);
            actual.TradingName.Should().Be(source.ProviderStandardLocation.TradingName);
            actual.Ukprn.Should().Be(source.ProviderStandardLocation.Ukprn);
            actual.ContactUrl.Should().Be(source.ProviderStandardLocation.ContactUrl);
            actual.Email.Should().Be(source.ProviderStandardLocation.Email);
            actual.Phone.Should().Be(source.ProviderStandardLocation.Phone);
            actual.AchievementRates.Should().NotBeEmpty();
            
        }
    }
}