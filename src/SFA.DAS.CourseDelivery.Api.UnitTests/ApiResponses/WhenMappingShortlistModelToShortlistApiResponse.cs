using AutoFixture.NUnit3;
using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Api.ApiResponses;
using SFA.DAS.CourseDelivery.Domain.Models;

namespace SFA.DAS.CourseDelivery.Api.UnitTests.ApiResponses
{
    public class WhenMappingShortlistModelToShortlistApiResponse
    {
        [Test, AutoData]
        public void Then_The_Fields_Are_Correctly_Mapped(Shortlist source)
        {
            var actual = (GetShortlistResponse) source;
            
            actual.Id.Should().Be(source.Id);
            actual.ShortlistUserId.Should().Be(source.ShortlistUserId);
            actual.ProviderUkprn.Should().Be(source.ProviderUkprn);
            actual.CourseId.Should().Be(source.CourseId);
            actual.LocationId.Should().Be(source.LocationId);
        }
    }
}