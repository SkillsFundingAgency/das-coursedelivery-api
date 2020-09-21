using AutoFixture.NUnit3;
using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.CourseDelivery.Domain.Models;

namespace SFA.DAS.CourseDelivery.Domain.UnitTests.Models
{
    public class WhenMappingFromProviderWithStandardAndLocationToProviderFeedbackAttribute
    {
        [Test, AutoData]
        public void Then_The_Fields_Are_Mapped(ProviderWithStandardAndLocation source)
        {
            var actual = (ProviderFeedbackAttribute) source;
            
            actual.AttributeName.Should().Be(source.AttributeName);
            actual.Strength.Should().Be(source.Strength);
            actual.Weakness.Should().Be(source.Weakness);
        }
    }
}