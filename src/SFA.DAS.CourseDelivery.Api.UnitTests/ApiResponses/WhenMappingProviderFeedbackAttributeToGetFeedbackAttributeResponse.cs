using AutoFixture.NUnit3;
using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Api.ApiResponses;
using SFA.DAS.CourseDelivery.Domain.Models;

namespace SFA.DAS.CourseDelivery.Api.UnitTests.ApiResponses
{
    public class WhenMappingProviderFeedbackAttributeToGetFeedbackAttributeResponse
    {
        [Test, AutoData]
        public void Then_The_Fields_Are_Mapped(ProviderFeedbackAttribute source)
        {
            var actual = (GetFeedbackAttributeResponse) source;
            
            actual.Should().BeEquivalentTo(source);
        }
    }
}