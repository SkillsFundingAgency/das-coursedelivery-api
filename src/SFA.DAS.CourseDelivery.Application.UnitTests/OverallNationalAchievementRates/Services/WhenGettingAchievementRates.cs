using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Application.OverallNationalAchievementRates.Services;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.CourseDelivery.Domain.Interfaces;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CourseDelivery.Application.UnitTests.OverallNationalAchievementRates.Services
{
    public class WhenGettingAchievementRates
    {
        [Test, MoqAutoData]
        public async Task Then_The_Repository_Is_Called_And_Data_Returned(
            string sectorSubjectArea,
            List<NationalAchievementRateOverall> items,
            [Frozen] Mock<INationalAchievementRateOverallRepository> repository,
            OverallNationalAchievementRateService service)
        {
            //Arrange
            repository.Setup(x => x.GetBySectorSubjectArea(sectorSubjectArea)).ReturnsAsync(items);
            
            //Act
            var actual = await service.GetItemsBySectorSubjectArea(sectorSubjectArea);
            
            //Assert
            actual.Should().BeEquivalentTo(items);
        }
    }
}