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
using SFA.DAS.CourseDelivery.Api.Controllers;
using SFA.DAS.CourseDelivery.Application.Shortlist.Commands.DeleteShortlistItemForUser;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CourseDelivery.Api.UnitTests.Controllers.Shortlist
{
    public class WhenDeletingShortlistForUserItem
    {
        [Test, RecursiveMoqAutoData]
        public async Task Then_Calls_Delete_Shortlist_From_Mediator(
            Guid userId,
            Guid id,
            [Frozen] Mock<IMediator> mockMediator,
            [Greedy] ShortlistController controller)
        {
            var controllerResult = await controller.DeleteShortlistItemForUser(userId, id) as ObjectResult;

            controllerResult!.StatusCode.Should().Be((int)HttpStatusCode.Accepted);
            mockMediator
                .Verify(mediator => mediator.Send(
                    It.Is<DeleteShortlistItemForUserRequest>(query =>
                        query.ShortlistUserId == userId && query.Id == id),
                    It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test, RecursiveMoqAutoData]
        public async Task And_Exception_Then_Returns_Bad_Request(
            Guid userId,
            Guid id,
            [Frozen] Mock<IMediator> mockMediator,
            [Greedy] ShortlistController controller)
        {
            mockMediator
                .Setup(mediator => mediator.Send(
                    It.IsAny<DeleteShortlistItemForUserRequest>(),
                    It.IsAny<CancellationToken>()))
                .Throws<InvalidOperationException>();

            var controllerResult = await controller.DeleteShortlistItemForUser(userId, id) as StatusCodeResult;

            controllerResult!.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        }
    }
}