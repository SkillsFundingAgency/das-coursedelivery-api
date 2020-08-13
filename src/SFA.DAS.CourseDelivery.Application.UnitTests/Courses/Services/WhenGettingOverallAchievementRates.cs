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
    public class WhenGettingOverallAchievementRates
    {
        [Test, RecursiveMoqAutoData]
        public async Task Then_Gets_OverallAchievementRates_By_Description(
            string description,
            List<Domain.Entities.NationalAchievementRateOverall> items,
            [Frozen]Mock<INationalAchievementRateOverallRepository> repository,
            ProviderService service)
        {
            //Arrange
            repository.Setup(x => x.GetBySectorSubjectArea(description)).ReturnsAsync(items);
            
            //Act
            var actual = await service.GetOverallAchievementRates(description);
            
            //Assert
            actual.Should().BeEquivalentTo(items);
        }
    }
}