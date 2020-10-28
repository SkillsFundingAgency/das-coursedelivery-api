using System.Collections.Generic;
using System.Linq;
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
    public class WhenGettingAProvider
    {
        [Test, RecursiveMoqAutoData]
        public async Task Then_Gets_The_Provider_From_The_Repository_With_Location_Information(
            int ukPrn,
            int standardId,
            double lat,
            double lon,
            Domain.Entities.ProviderWithStandardAndLocation provider,
            [Frozen]Mock<IProviderRepository> repository,
            ProviderService service)
        {
            //Arrange
            repository.Setup(x => x.GetProviderByStandardIdAndLocation(ukPrn,standardId, lat, lon)).ReturnsAsync(new List<Domain.Entities.ProviderWithStandardAndLocation>{provider});
            
            //Act
            var actual = await service.GetProviderByUkprnAndStandard(ukPrn, standardId, lat, lon);
            
            //Assert
            repository.Verify(x=>x.GetProviderByStandardIdAndLocation(ukPrn, standardId, lat, lon), Times.Once);
            actual.Should().NotBeNull();
            actual.Ukprn.Should().Be(provider.Ukprn);
            actual.Name.Should().Be(provider.Name);
        }

        [Test, RecursiveMoqAutoData]
        public async Task Then_Gets_The_Provider_From_The_Repository_Without_Location_Information(int ukPrn,
            int standardId,
            Domain.Entities.ProviderWithStandardAndLocation provider,
            [Frozen]Mock<IProviderRepository> repository,
            ProviderService service)
        {
            //Arrange
            repository.Setup(x => x.GetByUkprnAndStandardId(ukPrn, standardId)).ReturnsAsync(new List<Domain.Entities.ProviderWithStandardAndLocation>{provider});
            
            //Act
            var actual = await service.GetProviderByUkprnAndStandard(ukPrn, standardId, null, null);
            
            //Assert
            actual.Should().NotBeNull();
            actual.Ukprn.Should().Be(provider.Ukprn);
            actual.Name.Should().Be(provider.Name);
        }
    }
}