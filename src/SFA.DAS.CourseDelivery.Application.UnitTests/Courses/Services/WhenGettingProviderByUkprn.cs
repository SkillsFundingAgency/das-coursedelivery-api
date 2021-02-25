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
        public async Task Then_Provider_Is_Taken_From_ProviderRegistration_Repository(
            int ukprn,
            Domain.Entities.ProviderRegistration providerRegistration,
            [Frozen]Mock<IProviderRepository> repository,
            [Frozen]Mock<IProviderRegistrationRepository> providerRegistrationRepository,
            ProviderService service)
        {
            //Arrange
            providerRegistrationRepository.Setup(x => x.GetProviderByUkprn(ukprn))
                .ReturnsAsync(providerRegistration);
            
            //Act
            var actual = await service.GetProviderByUkprn(ukprn);
            
            //Assert
            actual.Should().BeEquivalentTo((ProviderSummary)providerRegistration);
        }
    }
}
