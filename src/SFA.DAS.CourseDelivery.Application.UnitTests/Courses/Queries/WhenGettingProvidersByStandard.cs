using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Application.Provider.Queries.ProvidersByCourse;
using SFA.DAS.CourseDelivery.Domain.Interfaces;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CourseDelivery.Application.UnitTests.Courses.Queries
{
    public class WhenGettingProvidersByStandard
    {
        [Test, RecursiveMoqAutoData]
        public async Task Then_Gets_Providers_From_The_Service_By_Standard_If_No_Location(
            GetCourseProvidersQuery query,
            List<Domain.Models.ProviderLocation> providers,
            [Frozen] Mock<IProviderService> providerService,
            GetCourseProvidersQueryHandler handler)
        {
            //Arrange
            query.Lat = null;
            query.Lon = null;
            providerService.Setup(x => x.GetProvidersByStandardId(query.StandardId, query.SectorSubjectArea)).ReturnsAsync(providers);

            //Act
            var actual = await handler.Handle(query, It.IsAny<CancellationToken>());

            //Assert
            actual.Providers.Should().BeEquivalentTo(providers);
        }

        [Test, RecursiveMoqAutoData]
        public async Task Then_If_There_Is_Location_Data_In_The_Request_It_Is_Filtered_By_Location_With_Sorting(
            GetCourseProvidersQuery query,
            List<Domain.Models.ProviderLocation> providers,
            [Frozen] Mock<IProviderService> providerService,
            GetCourseProvidersQueryHandler handler)
        {
            //Arrange
            providerService.Setup(x => x.GetProvidersByStandardAndLocation(query.StandardId, query.Lat.Value, query.Lon.Value, query.SortOrder, query.SectorSubjectArea)).ReturnsAsync(providers);
            
            //Act
            var actual = await handler.Handle(query, It.IsAny<CancellationToken>());

            //Assert
            actual.Providers.Should().BeEquivalentTo(providers);
        }
    }
}