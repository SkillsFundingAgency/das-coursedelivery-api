using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Application.Provider.Queries.Providers;
using SFA.DAS.CourseDelivery.Domain.Interfaces;
using SFA.DAS.CourseDelivery.Domain.Models;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CourseDelivery.Application.UnitTests.Courses.Queries
{
    public class WhenGettingProviders
    {
        [Test, MoqAutoData]
        public async Task Then_Gets_Providers_From_The_Service(
            GetProvidersQuery query,
            List<ProviderSummary> providersFromService,
            [Frozen] Mock<IProviderService> mockProviderService,
            GetProvidersQueryHandler handler)
        {
            //Arrange
            mockProviderService
                .Setup(x => x.GetRegisteredProviders())
                .ReturnsAsync(providersFromService);

            //Act
            var actual = await handler.Handle(query, It.IsAny<CancellationToken>());

            //Assert
            actual.RegisteredProviders.Should().BeEquivalentTo(providersFromService);
        }
    }
}