using System;
using System.Linq;
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
using SFA.DAS.CourseDelivery.Application.Shortlist.Queries.GetShortlistForUser;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CourseDelivery.Api.UnitTests.Controllers.Shortlist
{
    public class WhenGettingShortlistForUser
    {
        [Test, RecursiveMoqAutoData]
        public async Task Then_Gets_Shortlist_From_Mediator(
            Guid userId,
            GetShortlistForUserQueryResponse queryResult,
            [Frozen] Mock<IMediator> mockMediator,
            [Greedy] ShortlistController controller)
        {
            mockMediator
                .Setup(mediator => mediator.Send(
                    It.Is<GetShortlistForUserQuery>(query =>
                        query.UserId == userId),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(queryResult);

            var controllerResult = await controller.GetShortlistForUser(userId) as ObjectResult;

            controllerResult!.StatusCode.Should().Be((int)HttpStatusCode.OK);
            var model = controllerResult.Value as GetAllShortlistItemsForUserResponse;
            model!.ShortlistItems.Should().BeEquivalentTo(
                queryResult.Shortlist.Select(shortlist => (GetShortlistResponse) shortlist));
        }

        [Test, RecursiveMoqAutoData]
        public async Task And_Exception_Then_Returns_Bad_Request(
            Guid userId,
            [Frozen] Mock<IMediator> mockMediator,
            [Greedy] ShortlistController controller)
        {
            mockMediator
                .Setup(mediator => mediator.Send(
                    It.IsAny<GetShortlistForUserQuery>(),
                    It.IsAny<CancellationToken>()))
                .Throws<InvalidOperationException>();

            var controllerResult = await controller.GetShortlistForUser(userId) as StatusCodeResult;

            controllerResult!.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        }
    }
}