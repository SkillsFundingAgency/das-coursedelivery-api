using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.Testing.AutoFixture;
using Shortlist = SFA.DAS.CourseDelivery.Domain.Models.Shortlist;

namespace SFA.DAS.CourseDelivery.Domain.UnitTests.Models
{
    public class WhenMappingFromShortlistEntityToShortlistModel
    {
        [Test, RecursiveMoqAutoData]
        public void Then_The_Fields_Are_Correctly_Mapped(Domain.Entities.Shortlist source)
        {
            var actual = (Shortlist)source;

            actual.Id.Should().Be(source.Id);
            actual.ShortlistUserId.Should().Be(source.ShortlistUserId);
            actual.ProviderUkprn.Should().Be(source.ProviderUkprn);
            actual.CourseId.Should().Be(source.CourseId);
            //actual.LocationId.Should().Be(source.LocationId);
        }

        [Test]
        public void And_Null_Then_Returns_Null()
        {
            var actual = (Shortlist)null;

            actual.Should().BeNull();
        }
    }
}