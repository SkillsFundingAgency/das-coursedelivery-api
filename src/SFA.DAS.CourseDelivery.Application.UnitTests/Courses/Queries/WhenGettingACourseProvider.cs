using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Application.Provider.Queries.ProviderByCourse;
using SFA.DAS.CourseDelivery.Domain.Interfaces;
using SFA.DAS.CourseDelivery.Domain.Models;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CourseDelivery.Application.UnitTests.Courses.Queries
{
    public class WhenGettingACourseProvider
    {
        [Test, RecursiveMoqAutoData]
        public async Task Then_Gets_Provider_From_The_Service_By_Ukprn_And_StandardId(
            string sectorSubjectArea,
            GetCourseProviderQuery query,
            ProviderLocation providerStandard,
            [Frozen] Mock<IProviderService> providerService,
            GetCourseProviderQueryHandler handler)
        {
            //Arrange
            providerStandard.AchievementRates = providerStandard.AchievementRates.Select(c =>
            {
                c.SectorSubjectArea = sectorSubjectArea;
                return c;
            }).ToList();
            providerService.Setup(x => x.GetProviderByUkprnAndStandard(query.Ukprn, query.StandardId, query.Lat, query.Lon, query.SectorSubjectArea, query.ShortlistUserId.Value)).ReturnsAsync(providerStandard);
            
            //Act
            var actual = await handler.Handle(query, It.IsAny<CancellationToken>());

            //Assert
            actual.ProviderStandardLocation.Should().BeEquivalentTo(providerStandard);
        }
        
        [Test, RecursiveMoqAutoData]
        public async Task Then_Gets_Provider_From_The_Service_By_Ukprn_And_StandardId_And_Passes_Empty_Guid_If_No_Shortlist_UserId(
            string sectorSubjectArea,
            GetCourseProviderQuery query,
            ProviderLocation providerStandard,
            [Frozen] Mock<IProviderService> providerService,
            GetCourseProviderQueryHandler handler)
        {
            //Arrange
            query.ShortlistUserId = null;
            providerStandard.AchievementRates = null;
            providerService.Setup(x => x.GetProviderByUkprnAndStandard(query.Ukprn, query.StandardId, query.Lat, query.Lon, query.SectorSubjectArea, Guid.Empty)).ReturnsAsync(providerStandard);
            
            //Act
            var actual = await handler.Handle(query, It.IsAny<CancellationToken>());

            //Assert
            actual.ProviderStandardLocation.Should().BeEquivalentTo(providerStandard);
        }
        
    }
}