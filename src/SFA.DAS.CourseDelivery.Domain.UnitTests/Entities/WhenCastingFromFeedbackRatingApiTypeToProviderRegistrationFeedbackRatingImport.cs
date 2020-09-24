using AutoFixture.NUnit3;
using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.CourseDelivery.Domain.ImportTypes;

namespace SFA.DAS.CourseDelivery.Domain.UnitTests.Entities
{
    public class WhenCastingFromFeedbackRatingApiTypeToProviderRegistrationFeedbackRatingImport
    {
        [Test, AutoData]
        public void Then_Maps_All_Fields(int ukprn, FeedbackRating source)
        {
            var actual = new ProviderRegistrationFeedbackRatingImport().Map(ukprn, source);

            actual.Ukprn.Should().Be(ukprn);
            actual.FeedbackName.Should().Be(source.Key);
            actual.FeedbackCount.Should().Be(source.Value);
        }
    }
}