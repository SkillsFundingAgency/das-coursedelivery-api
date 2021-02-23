using System.Threading;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Application.Provider.Queries.Provider;
using SFA.DAS.CourseDelivery.Domain.Interfaces;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CourseDelivery.Application.UnitTests.Courses.Queries
{
    public class WhenGettingAProvider
    {
        [Test, RecursiveMoqAutoData]
        public async Task Then_Gets_Provider_From_The_Service_By_Ukprn(
            GetProviderQuery query,
            Domain.Models.ProviderSummary provider,
            [Frozen] Mock<IProviderService> providerService,
            GetProviderQueryHandler handler)
        {
            //Arrange
            providerService
                .Setup(x => x.GetProviderByUkprn(query.Ukprn))
                .ReturnsAsync(provider);

            //Act
            var actual = await handler.Handle(query, It.IsAny<CancellationToken>());

            //Assert
            actual.Provider.Should().BeEquivalentTo(provider);
        }
    }
}