using AutoFixture.NUnit3;
using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.CourseDelivery.Domain.Models;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CourseDelivery.Domain.UnitTests.Models
{
    public class WhenMappingFromProviderRegistrationFeedbackAttributeToProviderFeedbackAttribute
    {
        [Test, RecursiveMoqAutoData]
        public void Then_The_Fields_Are_Mapped(ProviderRegistrationFeedbackAttribute source)
        {
            var actual = (ProviderFeedbackAttribute) source;

            actual.Strength.Should().Be(source.Strength);
            actual.Weakness.Should().Be(source.Weakness);
            actual.AttributeName.Should().Be(source.AttributeName);
        }
    }
}