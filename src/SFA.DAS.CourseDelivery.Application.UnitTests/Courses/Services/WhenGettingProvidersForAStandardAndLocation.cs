using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Application.Provider.Services;
using SFA.DAS.CourseDelivery.Domain.Interfaces;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CourseDelivery.Application.UnitTests.Courses.Services
{
    public class WhenGettingProvidersForAStandardAndLocation
    {
        [Test, MoqAutoData]
        public async Task Then_The_Repository_Is_Called_And_List_Of_Ids_Returned(
            int standardId,
            double lat,
            double lon,
            List<int> providerIds,
            [Frozen] Mock<IProviderRepository> repository,
            ProviderService service)
        {
            //Arrange
            repository
                .Setup(x => x.GetUkprnsForStandardAndLocation(standardId, lat, lon))
                .ReturnsAsync(providerIds);
            
            //Act
            var actual = await service.GetUkprnsForStandardAndLocation(standardId, lat, lon);
            
            //Assert
            actual.Should().BeEquivalentTo(providerIds);
        }
    }
}