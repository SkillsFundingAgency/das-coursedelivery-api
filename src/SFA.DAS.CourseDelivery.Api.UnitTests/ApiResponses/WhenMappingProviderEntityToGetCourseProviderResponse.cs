using AutoFixture.NUnit3;
using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Api.ApiResponses;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CourseDelivery.Api.UnitTests.ApiResponses
{
    public class WhenMappingProviderEntityToGetCourseProviderResponse
    {
        [Test, RecursiveMoqAutoData]
        public void Then_Maps_Fields(Provider provider)
        {
            var actual = (GetProviderResponse) provider;

            actual.Ukprn.Should().Be(provider.Ukprn);
            actual.Name.Should().Be(provider.Name);
            actual.Email.Should().Be(provider.Email);
            actual.Website.Should().Be(provider.Website);
            actual.Phone.Should().Be(provider.Phone);
        }
    }
}