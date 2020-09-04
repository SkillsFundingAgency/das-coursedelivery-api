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
            var actual = (GetCourseProviderResponse) source;

            actual.Name.Should().Be(source.ProviderStandardContact.Provider.Name);
            actual.Ukprn.Should().Be(source.ProviderStandardContact.Provider.Ukprn);
            actual.ContactUrl.Should().Be(source.ProviderStandardContact.ContactUrl);
            actual.Email.Should().Be(source.ProviderStandardContact.Email);
            actual.Phone.Should().Be(source.ProviderStandardContact.Phone);
            actual.AchievementRates.Should().BeEquivalentTo(source.ProviderStandardContact.NationalAchievementRate, options => options
                .Excluding(c=>c.Provider)
                .Excluding(c=>c.ProviderStandard)
                .Excluding(c=>c.Id)
                .Excluding(c=>c.Age)
                .Excluding(c=>c.ApprenticeshipLevel)
            );
            
        }
    }
}