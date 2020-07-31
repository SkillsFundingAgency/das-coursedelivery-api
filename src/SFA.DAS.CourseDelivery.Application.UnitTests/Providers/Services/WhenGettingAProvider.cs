using System.Threading.Tasks;
using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Application.Provider.Services;
using SFA.DAS.CourseDelivery.Domain.Interfaces;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CourseDelivery.Application.UnitTests.Providers.Services
{
    public class WhenGettingAProvider
    {
        [Test, RecursiveMoqAutoData]
        public async Task Then_Gets_The_Provider_From_The_Repository(
            int ukPrn,
            Domain.Entities.Provider provider,
            [Frozen]Mock<IProviderRepository> repository,
            ProviderService service)
        {
            //Arrange
            repository.Setup(x => x.GetByUkprn(ukPrn)).ReturnsAsync(provider);
            
            //Act
            var actual = await service.GetProviderByUkprn(ukPrn);
            
            //Assert
            repository.Verify(x=>x.GetByUkprn(ukPrn), Times.Once);
            actual.Should().BeEquivalentTo(provider);
        }
    }
}