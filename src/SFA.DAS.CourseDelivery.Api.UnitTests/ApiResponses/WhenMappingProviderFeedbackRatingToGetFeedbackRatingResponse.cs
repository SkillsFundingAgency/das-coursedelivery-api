using AutoFixture.NUnit3;
using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Api.ApiResponses;
using SFA.DAS.CourseDelivery.Domain.Models;

namespace SFA.DAS.CourseDelivery.Api.UnitTests.ApiResponses
{
    public class WhenMappingProviderFeedbackRatingToGetFeedbackRatingResponse
    {
        [Test, AutoData]
        public void Then_The_Fields_Are_Mapped(ProviderFeedbackRating source)
        {
            var actual = (GetFeedbackRatingResponse) source;
            
            actual.Should().BeEquivalentTo(source);
        }
    }
}