using AutoFixture.NUnit3;
using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Domain.Entities;

namespace SFA.DAS.CourseDelivery.Domain.UnitTests.Entities
{
    public class WhenCastingProviderStandardLocationImportToProviderStandardLocationEntity
    {
        [Test, AutoData]
        public void Then_Maps_The_Fields(ProviderStandardLocationImport providerStandardLocationImport)
        {
            var actual = (ProviderStandardLocation) providerStandardLocationImport;
            
            actual.Should().BeEquivalentTo(providerStandardLocationImport);
        }
    }
}