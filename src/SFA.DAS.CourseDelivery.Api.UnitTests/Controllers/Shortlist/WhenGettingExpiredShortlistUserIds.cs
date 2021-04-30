using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Api.Controllers;
using SFA.DAS.CourseDelivery.Application.Shortlist.Queries.GetExpiredShortlistUsers;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CourseDelivery.Api.UnitTests.Controllers.Shortlist
{
    public class WhenGettingExpiredShortlistUserIds
    {
        [Test, RecursiveMoqAutoData]
        public async Task Then_Gets_Shortlist_UserIds_From_Mediator(
            uint expiryPeriodInDays,
            GetExpiredShortlistUsersQueryResult result,
            [Frozen]Mock<IMediator> mockMediator,
            [Greedy] ShortlistController controller)
        {
            mockMediator
                .Setup(mediator => mediator.Send(
                    It.Is<GetExpiredShortlistUsersQuery>(c => c.ExpiryInDays.Equals(expiryPeriodInDays)),
                    It.IsAny<CancellationToken>())).ReturnsAsync(result);

            var actual = await controller.GetExpiredShortlistUserIds(expiryPeriodInDays) as ObjectResult;
            
            actual!.StatusCode.Should().Be((int)HttpStatusCode.OK);
            var actualModel = actual.Value as List<Guid>;
            actualModel.Should().BeEquivalentTo(result.UserIds);
        }

        [Test, MoqAutoData]
        public async Task And_Exception_Then_Returns_InternalServerError(
            uint expiryPeriodInDays,
            [Frozen]Mock<IMediator> mockMediator,
            [Greedy] ShortlistController controller)
        {
            mockMediator
                .Setup(mediator => mediator.Send(
                    It.Is<GetExpiredShortlistUsersQuery>(c => c.ExpiryInDays.Equals(expiryPeriodInDays)),
                    It.IsAny<CancellationToken>())).ThrowsAsync(new Exception());

            var actual = await controller.GetExpiredShortlistUserIds(expiryPeriodInDays) as StatusCodeResult;
            
            actual!.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);
        }
    }
}