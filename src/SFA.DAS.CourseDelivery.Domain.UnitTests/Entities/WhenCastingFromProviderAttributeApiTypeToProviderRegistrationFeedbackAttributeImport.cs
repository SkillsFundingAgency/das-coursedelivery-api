using AutoFixture.NUnit3;
using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.CourseDelivery.Domain.ImportTypes;

namespace SFA.DAS.CourseDelivery.Domain.UnitTests.Entities
{
    public class WhenCastingFromProviderAttributeApiTypeToProviderRegistrationFeedbackAttributeImport
    {
        [Test, AutoData]
        public void Then_Maps_All_Fields(int ukprn, ProviderAttribute source)
        {
            var actual = new ProviderRegistrationFeedbackAttributeImport().Map(ukprn, source);

            actual.Ukprn.Should().Be(ukprn);
            actual.AttributeName.Should().Be(source.Name);
            actual.Strength.Should().Be(source.Strengths);
            actual.Weakness.Should().Be(source.Weaknesses);
        }
    }
}