using AutoFixture.NUnit3;
using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Domain.Entities;
using ProviderRegistration = SFA.DAS.CourseDelivery.Domain.ImportTypes.ProviderRegistration;

namespace SFA.DAS.CourseDelivery.Domain.UnitTests.Entities
{
    public class WhenCastingFromProviderRegistrationApiTypeToProviderRegistrationImport
    {
        [Test, AutoData]
        public void Then_Maps_All_Fields(ProviderRegistration source)
        {
            var result = (ProviderRegistrationImport) source;

            result.Should().BeEquivalentTo(source, options=>options.Excluding(c=>c.Feedback));
            result.FeedbackTotal.Should().Be(source.Feedback.Total);
        }
    }
}
