using System.Threading;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Application.Shortlist.Queries.GetShortlistForUserCount;
using SFA.DAS.CourseDelivery.Domain.Interfaces;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CourseDelivery.Application.UnitTests.Shortlist.Queries
{
    public class WhenGettingShortlistItemCountForUser
    {
        [Test, MoqAutoData]
        public async Task Then_Gets_Shortlist_Item_Count_From_The_Services(
            GetShortlistForUserCountQuery query,
            int returnValue,
            [Frozen] Mock<IShortlistService> mockShortlistService,
            GetShortlistForUserCountQueryHandler handler)
        {
            //Arrange
            mockShortlistService
                .Setup(x => x.GetShortlistItemCountForUser(query.UserId))
                .ReturnsAsync(returnValue);
            
            //Act
            var actual = await handler.Handle(query, It.IsAny<CancellationToken>());

            //Assert
            actual.Should().Be(returnValue);
        }
    }
}