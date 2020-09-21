using AutoFixture.NUnit3;
using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Domain.Entities;

namespace SFA.DAS.CourseDelivery.Domain.UnitTests.Entities
{
    public class WhenCastingFromProviderRegistrationFeedbackRatingImportToProviderRegistrationFeedbackRating
    {
        [Test, AutoData]
        public void Then_The_Fields_Are_Mapped(ProviderRegistrationFeedbackRatingImport source)
        {
            var actual = (ProviderRegistrationFeedbackRating) source;

            actual.Should().BeEquivalentTo(source);
        }
    }
}