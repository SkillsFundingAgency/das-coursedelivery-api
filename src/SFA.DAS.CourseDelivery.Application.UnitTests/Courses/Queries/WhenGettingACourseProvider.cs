using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Application.Provider.Queries.ProviderByCourse;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.CourseDelivery.Domain.Interfaces;
using SFA.DAS.CourseDelivery.Domain.Models;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CourseDelivery.Application.UnitTests.Courses.Queries
{
    public class WhenGettingACourseProvider
    {
        [Test, RecursiveMoqAutoData]
        public async Task Then_Gets_Provider_From_The_Service_By_Ukprn_StandardId_And_Location_And_Overall_AchievementRates(
            string sectorSubjectArea,
            GetCourseProviderQuery query,
            ProviderLocation providerStandard,
            List<NationalAchievementRateOverall> overallAchievementRates,
            [Frozen] Mock<IProviderService> providerService,
            GetCourseProviderQueryHandler handler)
        {
            //Arrange
            providerStandard.AchievementRates = providerStandard.AchievementRates.Select(c =>
            {
                c.SectorSubjectArea = sectorSubjectArea;
                return c;
            }).ToList();
            providerService.Setup(x => x.GetProviderByUkprnAndStandard(query.Ukprn, query.StandardId, query.Lat, query.Lon)).ReturnsAsync(providerStandard);
            providerService
                .Setup(x => x.GetOverallAchievementRates(sectorSubjectArea)).ReturnsAsync(overallAchievementRates);

            //Act
            var actual = await handler.Handle(query, It.IsAny<CancellationToken>());

            //Assert
            actual.ProviderStandardLocation.Should().BeEquivalentTo(providerStandard);
        }
        
        [Test, RecursiveMoqAutoData]
        public async Task Then_Gets_Provider_From_The_Service_By_Ukprn_And_StandardId_And_Returns_Null_For_Overall_AchievementRates_If_No_AchievementRate(
            string sectorSubjectArea,
            GetCourseProviderQuery query,
            ProviderLocation providerStandard,
            [Frozen] Mock<IProviderService> providerService,
            GetCourseProviderQueryHandler handler)
        {
            //Arrange
            providerStandard.AchievementRates = null;
            providerService.Setup(x => x.GetProviderByUkprnAndStandard(query.Ukprn, query.StandardId, query.Lat, query.Lon)).ReturnsAsync(providerStandard);
            
            //Act
            var actual = await handler.Handle(query, It.IsAny<CancellationToken>());

            //Assert
            actual.ProviderStandardLocation.Should().BeEquivalentTo(providerStandard);
        }
        
    }
}