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
    public class WhenGettingProvidersForACourse
    {
        [Test, RecursiveMoqAutoData]
        public async Task Then_Gets_The_Providers_For_A_Course_From_The_Repository(
            int standardId,
            List<Domain.Entities.Provider> providers,
            [Frozen]Mock<IProviderRepository> repository,
            ProviderService service)
        {
            //Arrange
            repository.Setup(x => x.GetByStandardId(standardId)).ReturnsAsync(providers);
            
            //Act
            var actual = await service.GetProvidersByStandardId(standardId);
            
            //Assert
            repository.Verify(x=>x.GetByStandardId(standardId), Times.Once);
            actual.Should().BeEquivalentTo(providers);
        }
    }
}