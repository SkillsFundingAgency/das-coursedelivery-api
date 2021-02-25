using System.Collections.Generic;
using System.Linq;
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
    public class WhenGettingAllRegisteredProviders
    {
        [Test, RecursiveMoqAutoData]
        public async Task Then_Gets_The_Providers_From_The_Repository(
            List<Domain.Entities.ProviderRegistration> providersFromRepository,
            [Frozen]Mock<IProviderRegistrationRepository> mockRepository,
            ProviderService service)
        {
            //Arrange
            mockRepository
                .Setup(repository => repository.GetAllProviders())
                .ReturnsAsync(providersFromRepository);

            //Act
            var actual = await service.GetRegisteredProviders();
            
            //Assert
            actual.Should().BeEquivalentTo(providersFromRepository.Select(provider => (ProviderSummary)provider));
        }
    }
}