using AutoFixture.NUnit3;
using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.CourseDelivery.Domain.ImportTypes;

namespace SFA.DAS.CourseDelivery.Domain.UnitTests.Entities
{
    public class WhenCastingFromProviderApiTypeToProviderImport
    {
        [Test, AutoData]
        public void Then_Maps_The_Fields(ImportTypes.Provider provider)
        {
            var actual = (ProviderImport) provider;
            
            actual.Should().BeEquivalentTo(provider, options => options
                .Excluding(c=>c.Locations)
                .Excluding(c=>c.Standards)
                );
        }
    }
}