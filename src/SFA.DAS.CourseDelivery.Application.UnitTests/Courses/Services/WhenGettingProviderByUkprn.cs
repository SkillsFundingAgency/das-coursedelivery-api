using System.Threading.Tasks;
using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Application.Provider.Services;
using SFA.DAS.CourseDelivery.Domain.Interfaces;
using SFA.DAS.CourseDelivery.Domain.Models;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CourseDelivery.Application.UnitTests.Courses.Services
{
    public class WhenGettingProviderByUkprn
    {
        [Test, RecursiveMoqAutoData]
        public async Task Then_Gets_Provider_From_Repository(
            int ukprn,
            Domain.Entities.Provider provider,
            [Frozen]Mock<IProviderRepository> repository,
            [Frozen]Mock<IProviderRegistrationRepository> providerRegistrationRepository,
            ProviderService service)
        {
            //Arrange
            repository
                .Setup(x => x.GetByUkprn(ukprn))
                .ReturnsAsync(provider);
            providerRegistrationRepository.Verify(x=>x.GetByUkprn(It.IsAny<int>()), Times.Never);

            //Act
            var actual = await service.GetProviderByUkprn(ukprn);

            //Assert
            actual.Should().BeEquivalentTo((ProviderSummary)provider);
        }

        [Test, RecursiveMoqAutoData]
        public async Task Then_If_Null_From_ProviderRepository_It_Is_Taken_From_ProviderRegistration(
            int ukprn,
            Domain.Entities.ProviderRegistration providerRegistration,
            [Frozen]Mock<IProviderRepository> repository,
            [Frozen]Mock<IProviderRegistrationRepository> providerRegistrationRepository,
            ProviderService service)
        {
            //Arrange
            repository
                .Setup(x => x.GetByUkprn(ukprn))
                .ReturnsAsync((Domain.Entities.Provider) null);
            providerRegistrationRepository.Setup(x => x.GetByUkprn(ukprn))
                .ReturnsAsync(providerRegistration);
            
            //Act
            var actual = await service.GetProviderByUkprn(ukprn);
            
            //Assert
            actual.Should().BeEquivalentTo((ProviderSummary)providerRegistration);
        }
    }
}
