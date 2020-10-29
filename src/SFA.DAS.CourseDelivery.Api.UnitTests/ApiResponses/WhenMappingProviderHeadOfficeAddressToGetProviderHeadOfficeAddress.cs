using AutoFixture.NUnit3;
using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Api.ApiResponses;
using SFA.DAS.CourseDelivery.Domain.Models;

namespace SFA.DAS.CourseDelivery.Api.UnitTests.ApiResponses
{
    public class WhenMappingProviderHeadOfficeAddressToGetProviderHeadOfficeAddress
    {
        [Test, AutoData]
        public void Then_Maps_Fields(ProviderHeadOfficeAddress source)
        {
            var actual = (GetProviderHeadOfficeAddress) source;
            
            actual.Should().BeEquivalentTo(source);
        }
    }
}