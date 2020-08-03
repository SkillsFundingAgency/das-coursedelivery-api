using AutoFixture.NUnit3;
using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Domain.Entities;

namespace SFA.DAS.CourseDelivery.Domain.UnitTests.Entities
{
    public class WhenCastingProviderImportToProviderEntity
    {
        [Test, AutoData]
        public void Then_Maps_The_Fields(ProviderImport providerImport)
        {
            var actual = (Provider) providerImport;
            
            actual.Should().BeEquivalentTo(providerImport);
        }
    }
}