using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Application.Provider.Queries.CoursesByProvider;
using SFA.DAS.CourseDelivery.Domain.Interfaces;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CourseDelivery.Application.UnitTests.Courses.Queries
{
    public class WhenGettingCoursesByUkprn
    {
        [Test, RecursiveMoqAutoData]
        public async Task Then_Gets_Providers_From_The_Service_By_Standard(
            GetProviderCoursesQuery query,
            List<int> standardIds,
            [Frozen] Mock<IProviderService> providerService,
            GetProviderCoursesQueryHandler handler)
        {
            //Arrange
            providerService.Setup(x => x.GetStandardIdsByUkprn(query.Ukprn)).ReturnsAsync(standardIds);

            //Act
            var actual = await handler.Handle(query, It.IsAny<CancellationToken>());

            //Assert
            actual.CourseIds.Should().BeEquivalentTo(standardIds);
        }
    }
}