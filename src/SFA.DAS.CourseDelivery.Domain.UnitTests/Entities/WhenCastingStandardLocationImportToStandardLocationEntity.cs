using AutoFixture.NUnit3;
using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Domain.Entities;

namespace SFA.DAS.CourseDelivery.Domain.UnitTests.Entities
{
    public class WhenCastingStandardLocationImportToStandardLocationEntity
    {
        [Test, AutoData]
        public void Then_Maps_The_Fields(StandardLocationImport standardLocationImport)
        {
            var actual = (StandardLocation) standardLocationImport;
            
            actual.Should().BeEquivalentTo(standardLocationImport);
        }
    }
}