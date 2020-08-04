using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Api.ApiResponses;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CourseDelivery.Api.UnitTests.ApiResponses
{
    public class WhenMappingProviderStandardEntityToGetCourseProviderResponse
    {
        [Test, RecursiveMoqAutoData]
        public void Then_Maps_Fields(ProviderStandard source)
        {
            var actual = (GetCourseProviderResponse) source;

            actual.Name.Should().Be(source.Provider.Name);
            actual.Ukprn.Should().Be(source.Provider.Ukprn);
            actual.ContactUrl.Should().Be(source.ContactUrl);
            actual.Email.Should().Be(source.Email);
            actual.Phone.Should().Be(source.Phone);
        }
    }
}