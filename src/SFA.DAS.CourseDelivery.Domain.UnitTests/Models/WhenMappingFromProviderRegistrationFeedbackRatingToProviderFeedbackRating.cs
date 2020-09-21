using AutoFixture.NUnit3;
using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.CourseDelivery.Domain.Models;

namespace SFA.DAS.CourseDelivery.Domain.UnitTests.Models
{
    public class WhenMappingFromProviderRegistrationFeedbackRatingToProviderFeedbackRating
    {
        [Test, AutoData]
        public void Then_The_Fields_Are_Mapped(ProviderRegistrationFeedbackRating source)
        {
            var actual = (ProviderFeedbackRating) source;
            
            actual.FeedbackName.Should().Be(source.FeedbackName);
            actual.FeedbackCount.Should().Be(source.FeedbackCount);
        }
    }
}