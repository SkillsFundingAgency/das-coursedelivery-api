using AutoFixture.NUnit3;
using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Api.ApiResponses;
using SFA.DAS.CourseDelivery.Domain.Models;

namespace SFA.DAS.CourseDelivery.Api.UnitTests.ApiResponses
{
    public class WhenMappingProviderSummaryAddressToGetProviderSummaryAddress
    {
        [Test]
        public void And_Null_Source_Then_Returns_Null()
        {
            var actual = (GetProviderSummaryAddress)null;
            
            actual.Should().BeNull();
        }

        [Test, AutoData]
        public void Then_Maps_Fields(ProviderSummaryAddress source)
        {
            var actual = (GetProviderSummaryAddress) source;
            
            actual.Should().BeEquivalentTo(source);
        }
    }
}