using AutoFixture.NUnit3;
using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Domain.Entities;

namespace SFA.DAS.CourseDelivery.Domain.UnitTests.Entities
{
    public class WhenCastingFromProviderRegistrationFeedbackAttributeImportToProviderRegistrationFeedbackAttribute
    {
        [Test, AutoData]
        public void Then_The_Fields_Are_Mapped(ProviderRegistrationFeedbackAttributeImport source)
        {
            var actual = (ProviderRegistrationFeedbackAttribute) source;

            actual.Should().BeEquivalentTo(source);
        }
    }
}