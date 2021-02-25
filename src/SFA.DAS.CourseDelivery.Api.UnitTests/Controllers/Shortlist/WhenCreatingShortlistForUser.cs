using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Api.ApiRequests;
using SFA.DAS.CourseDelivery.Api.Controllers;
using SFA.DAS.CourseDelivery.Application.Shortlist.Commands.CreateShortlistItemForUser;
using SFA.DAS.Testing.AutoFixture;
using ValidationResult = SFA.DAS.CourseDelivery.Domain.Validation.ValidationResult;

namespace SFA.DAS.CourseDelivery.Api.UnitTests.Controllers.Shortlist
{
    public class WhenCreatingShortlistForUser
    {
        [Test, RecursiveMoqAutoData]
        public async Task Then_Gets_Shortlist_From_Mediator(
            string controllerName,
            Guid returnId,
            CreateShortlistRequest request,
            [Frozen] Mock<HttpContext> httpContext,
            [Frozen] Mock<IMediator> mockMediator)
        {
            httpContext = new Mock<HttpContext>();
            var controller = new ShortlistController(mockMediator.Object, Mock.Of<ILogger<ShortlistController>>())
            {
                ControllerContext = {HttpContext = httpContext.Object,
                    ActionDescriptor = new ControllerActionDescriptor
                    {
                        ControllerName = controllerName
                    }}
            };
            mockMediator
                .Setup(mediator => mediator.Send(
                    It.Is<CreateShortlistItemForUserRequest>(query =>
                        query.ShortlistUserId == request.ShortlistUserId
                        && query.Lat.Equals(request.Lat)
                        && query.Lon.Equals(request.Lon)
                        && query.StandardId == request.StandardId
                        && query.LocationDescription == request.LocationDescription
                        && query.Ukprn == request.Ukprn
                        && query.SectorSubjectArea == request.SectorSubjectArea

                    ),
                    It.IsAny<CancellationToken>())).ReturnsAsync(returnId);
            
            var controllerResult = await controller.CreateShortlistItemForUser(request) as CreatedResult;

            controllerResult!.StatusCode.Should().Be((int)HttpStatusCode.Created);
            controllerResult.Location.Should().Be($"/api/{controllerName}/{request.ShortlistUserId}/items/{returnId}");
            

        }

        [Test, RecursiveMoqAutoData]
        public async Task And_Validation_Exception_Then_Returns_BadRequest(
            string errorKey,
            CreateShortlistRequest request,
            [Frozen] Mock<IMediator> mockMediator,
            [Greedy] ShortlistController controller)
        {
            var validationResult = new ValidationResult{ValidationDictionary = { {errorKey,"Some error"}}};
            mockMediator
                .Setup(mediator => mediator.Send(
                    It.IsAny<CreateShortlistItemForUserRequest>(),
                    It.IsAny<CancellationToken>()))
                .Throws(new ValidationException(validationResult.DataAnnotationResult, null, null));

            var controllerResult = await controller.CreateShortlistItemForUser(request) as ObjectResult;

            controllerResult!.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
            controllerResult.Value.ToString().Should().Contain(errorKey);
        }

        [Test, RecursiveMoqAutoData]
        public async Task And_Exception_Then_Returns_Error(
            CreateShortlistRequest request,
            [Frozen] Mock<IMediator> mockMediator,
            [Greedy] ShortlistController controller)
        {
            mockMediator
                .Setup(mediator => mediator.Send(
                    It.IsAny<CreateShortlistItemForUserRequest>(),
                    It.IsAny<CancellationToken>()))
                .Throws<InvalidOperationException>();

            var controllerResult = await controller.CreateShortlistItemForUser(request) as StatusCodeResult;

            controllerResult!.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);
        }
    }
}