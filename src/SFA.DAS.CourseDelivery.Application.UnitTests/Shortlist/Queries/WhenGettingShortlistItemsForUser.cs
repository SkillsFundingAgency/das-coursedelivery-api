using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Application.Shortlist.Queries.GetShortlistItemsForUser;
using SFA.DAS.CourseDelivery.Domain.Interfaces;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CourseDelivery.Application.UnitTests.Shortlist.Queries
{
    public class WhenGettingShortlistItemsForUser
    {
        [Test, MoqAutoData]
        public async Task Then_Gets_ShortlistItems_From_The_Service(
            GetShortlistItemsForUserQuery query,
            List<Domain.Models.Shortlist> shortlistItemsFromService,
            [Frozen] Mock<IShortlistService> mockShortlistService,
            GetShortlistItemsForUserQueryHandler handler)
        {
            //Arrange
            mockShortlistService
                .Setup(x => x.GetAllForUser(query.UserId))
                .ReturnsAsync(shortlistItemsFromService);

            //Act
            var actual = await handler.Handle(query, It.IsAny<CancellationToken>());

            //Assert
            actual.ShortlistItems.Should().BeEquivalentTo(shortlistItemsFromService);
        }
    }
}