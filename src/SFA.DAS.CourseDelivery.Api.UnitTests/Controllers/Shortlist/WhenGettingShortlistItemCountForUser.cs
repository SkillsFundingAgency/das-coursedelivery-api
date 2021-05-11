using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Api.ApiResponses;
using SFA.DAS.CourseDelivery.Api.Controllers;
using SFA.DAS.CourseDelivery.Application.Shortlist.Queries.GetShortlistForUserCount;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CourseDelivery.Api.UnitTests.Controllers.Shortlist
{
    public class WhenGettingShortlistItemCountForUser
    {
        [Test, MoqAutoData]
        public async Task Then_Gets_Shortlist_Item_Count_From_Mediator(
            Guid userId,
            int queryResult,
            [Frozen] Mock<IMediator> mockMediator,
            [Greedy] ShortlistController controller)
        {
            mockMediator
                .Setup(mediator => mediator.Send(
                    It.Is<GetShortlistForUserCountQuery>(query =>
                        query.UserId == userId),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(queryResult);

            var controllerResult = await controller.GetShortlistForUserCount(userId) as ObjectResult;

            controllerResult!.StatusCode.Should().Be((int)HttpStatusCode.OK);
            var model = controllerResult.Value as GetShortlistForUserCountResponse;
            model!.Count.Should().Be(queryResult);
        }

        [Test, MoqAutoData]
        public async Task And_Exception_Then_Returns_Bad_Request(
            Guid userId,
            [Frozen] Mock<IMediator> mockMediator,
            [Greedy] ShortlistController controller)
        {
            mockMediator
                .Setup(mediator => mediator.Send(
                    It.IsAny<GetShortlistForUserCountQuery>(),
                    It.IsAny<CancellationToken>()))
                .Throws<InvalidOperationException>();

            var controllerResult = await controller.GetShortlistForUserCount(userId) as StatusCodeResult;

            controllerResult!.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        }
    }
}