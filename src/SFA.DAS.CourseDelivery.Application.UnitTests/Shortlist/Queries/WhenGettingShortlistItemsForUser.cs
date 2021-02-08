using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Application.Shortlist.Queries.GetShortlistItemsForUser;
using SFA.DAS.CourseDelivery.Domain.Interfaces;
using SFA.DAS.CourseDelivery.Domain.Models;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CourseDelivery.Application.UnitTests.Shortlist.Queries
{
    public class WhenGettingShortlistItemsForUser
    {
        [Test, MoqAutoData]
        public async Task Then_Gets_ShortlistItems_From_The_Service(
            GetShortlistItemsForUserQuery query,
            List<Domain.Models.Shortlist> shortlistFromService,
            ProviderLocation providerLocationFromService,
            [Frozen] Mock<IShortlistService> mockShortlistService,
            [Frozen] Mock<IProviderService> mockProviderService,
            GetShortlistItemsForUserQueryHandler handler)
        {
            //Arrange
            mockShortlistService
                .Setup(x => x.GetAllForUser(query.UserId))
                .ReturnsAsync(shortlistFromService);
            mockProviderService
                .Setup(service => service.GetProviderByUkprnAndStandard(
                    shortlistFromService[0].ProviderUkprn, 
                    shortlistFromService[0].CourseId, 
                    shortlistFromService[0].Lat,
                    shortlistFromService[0].Long,
                    null))
                .ReturnsAsync(providerLocationFromService);

            //Act
            var actual = await handler.Handle(query, It.IsAny<CancellationToken>());

            //Assert
            actual.ShortlistItems.Should().BeEquivalentTo(shortlistFromService);
        }
    }
}