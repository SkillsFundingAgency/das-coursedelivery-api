using AutoFixture.NUnit3;
using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Domain.Entities;

namespace SFA.DAS.CourseDelivery.Domain.UnitTests.Entities
{
    public class WhenCastingProviderStandardImportToProviderStandardEntity
    {
        [Test, AutoData]
        public void Then_Maps_The_Fields(ProviderStandardImport providerStandardImport)
        {
            var actual = (ProviderStandard) providerStandardImport;
            
            actual.Should().BeEquivalentTo(providerStandardImport);
        }
    }
}