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
    public class WhenGettingProviderByUkprn
    {
        [Test, RecursiveMoqAutoData]
        public async Task Then_Gets_Provider_From_Repository(
            int ukprn,
            Domain.Entities.Provider provider,
            [Frozen]Mock<IProviderRepository> repository,
            ProviderService service)
        {
            //Arrange
            repository
                .Setup(x => x.GetByUkprn(ukprn))
                .ReturnsAsync(provider);

            //Act
            var actual = await service.GetProviderByUkprn(ukprn);

            //Assert
            actual.Should().BeEquivalentTo(provider);
        }
    }
}
