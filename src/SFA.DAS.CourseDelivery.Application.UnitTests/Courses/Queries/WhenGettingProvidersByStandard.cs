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
        public async Task Then_Gets_Providers_From_The_Service_By_Standard(
            GetCourseProvidersQuery query,
            List<Domain.Entities.Provider> providers,
            [Frozen] Mock<IProviderService> providerService,
            GetCourseProvidersQueryHandler handler)
        {
            //Arrange
            providerService.Setup(x => x.GetProvidersByStandardId(query.StandardId)).ReturnsAsync(providers);

            //Act
            var actual = await handler.Handle(query, It.IsAny<CancellationToken>());

            //Assert
            actual.Providers.Should().BeEquivalentTo(providers);
        }
    }
}