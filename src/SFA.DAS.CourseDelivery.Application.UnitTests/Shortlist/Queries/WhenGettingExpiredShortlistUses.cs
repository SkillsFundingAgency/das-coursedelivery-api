using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Application.Shortlist.Queries.GetExpiredShortlistUsers;
using SFA.DAS.CourseDelivery.Domain.Interfaces;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CourseDelivery.Application.UnitTests.Shortlist.Queries
{
    public class WhenGettingExpiredShortlistUses
    {
        [Test, MoqAutoData]
        public async Task Then_The_Service_Is_Called_And_Ids_Returned(
            GetExpiredShortlistUsersQuery query,
            List<Guid> userIds,
            [Frozen] Mock<IShortlistService> service,
            GetExpiredShortlistUsersQueryHandler handler)
        {
            //Arrange
            service.Setup(x => x.GetExpiredShortlistUserIds(query.ExpiryInDays)).ReturnsAsync(userIds);
            
            //Act
            var actual = await handler.Handle(query, CancellationToken.None);
            
            //Assert
            actual.UserIds.Should().BeEquivalentTo(userIds);
        }
    }
}