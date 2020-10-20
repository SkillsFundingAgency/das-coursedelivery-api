using AutoFixture.NUnit3;
using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Domain.Entities;

namespace SFA.DAS.CourseDelivery.Domain.UnitTests.Entities
{
    public class WhenCastingFromProviderRegistrationImportToProviderRegistration
    {
        [Test, AutoData]
        public void Then_Maps_All_Fields(ProviderRegistrationImport source)
        {
            var result = (ProviderRegistration) source;

            result.Should().BeEquivalentTo(source);
        }
    }
}