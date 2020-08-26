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
    public class WhenGettingProvidersForACourseAndLocation
    {
        [Test, RecursiveMoqAutoData]
        public async Task Then_Gets_The_Providers_For_A_Course_And_Location_From_The_Repository(
            int standardId,
            double lat, 
            double lon,
            List<Domain.Entities.ProviderWithStandardAndLocation> providers,
            [Frozen]Mock<IProviderRepository> repository,
            ProviderService service)
        {
            //Arrange
            repository.Setup(x => x.GetByStandardIdAndLocation(standardId, lat, lon)).ReturnsAsync(providers);
            
            //Act
            var actual = await service.GetProvidersByStandardAndLocation(standardId, lat, lon);
            
            //Assert
            repository.Verify(x=>x.GetByStandardIdAndLocation(standardId, lat, lon), Times.Once);
            actual.Should().BeEquivalentTo(providers, options=> options.ExcludingMissingMembers());
        }
    }
}