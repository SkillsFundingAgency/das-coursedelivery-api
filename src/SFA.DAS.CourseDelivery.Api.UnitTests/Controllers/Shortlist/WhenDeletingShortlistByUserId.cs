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
using SFA.DAS.CourseDelivery.Application.Shortlist.Commands.DeleteShortlistForUser;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CourseDelivery.Api.UnitTests.Controllers.Shortlist
{
    public class WhenDeletingShortlistByUserId
    {
        [Test, MoqAutoData]
        public async Task Then_Calls_Mediator_Passing_The_Id(
            Guid shortlistUserId,
            [Frozen]Mock<IMediator> mockMediator,
            [Greedy] ShortlistController controller)
        {
            var actual = await controller.DeleteShortlistForUser(shortlistUserId) as ObjectResult;

            Assert.IsNotNull(actual);
            actual.StatusCode.Should().Be((int) HttpStatusCode.Accepted);
            mockMediator.Verify(
                x => x.Send(It.Is<DeleteShortlistForUserCommand>(c => c.ShortlistUserId.Equals(shortlistUserId)),
                    It.IsAny<CancellationToken>()), Times.Once);
        }
        
        [Test, MoqAutoData]
        public async Task And_Exception_Then_Returns_InternalServerError(
            Guid shortlistUserId,
            [Frozen]Mock<IMediator> mockMediator,
            [Greedy] ShortlistController controller)
        {
            mockMediator.Setup(
                x => x.Send(It.Is<DeleteShortlistForUserCommand>(c => c.ShortlistUserId.Equals(shortlistUserId)),
                    It.IsAny<CancellationToken>())).ThrowsAsync(new Exception());

            var actual = await controller.DeleteShortlistForUser(shortlistUserId) as StatusCodeResult;
            
            actual!.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);
        }
    }
}