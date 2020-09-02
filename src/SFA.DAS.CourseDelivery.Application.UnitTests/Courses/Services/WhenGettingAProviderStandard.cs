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
    public class WhenGettingAProviderStandard
    {
        [Test, RecursiveMoqAutoData]
        public async Task Then_Gets_The_ProviderStandard_From_The_Repository(
            int ukPrn,
            int standardId,
            Domain.Entities.ProviderStandard provider,
            [Frozen]Mock<IProviderStandardRepository> repository,
            ProviderService service)
        {
            //Arrange
            repository.Setup(x => x.GetByUkprnAndStandard(ukPrn,standardId)).ReturnsAsync(provider);
            
            //Act
            var actual = await service.GetProviderByUkprnAndStandard(ukPrn, standardId);
            
            //Assert
            repository.Verify(x=>x.GetByUkprnAndStandard(ukPrn, standardId), Times.Once);
            actual.Should().BeEquivalentTo(provider);
        }
    }
}