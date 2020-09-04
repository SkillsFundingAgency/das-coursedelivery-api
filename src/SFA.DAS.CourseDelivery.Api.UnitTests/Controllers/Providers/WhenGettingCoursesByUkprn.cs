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
using SFA.DAS.CourseDelivery.Application.Provider.Queries.CoursesByProvider;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CourseDelivery.Api.UnitTests.Controllers.Providers
{
    public class WhenGettingCoursesByUkprn
    {
        [Test, RecursiveMoqAutoData]
        public async Task Then_Gets_StandardId_List_From_Mediator(
            int ukprn,
            GetProviderCoursesQueryResponse queryResult,
            [Frozen] Mock<IMediator> mockMediator,
            [Greedy] ProvidersController controller)
        {
            mockMediator
                .Setup(mediator => mediator.Send(
                    It.Is<GetProviderCoursesQuery>(query =>
                        query.Ukprn == ukprn),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(queryResult);

            var controllerResult = await controller.GetStandardIdsByProvider(ukprn) as ObjectResult;

            var model = controllerResult.Value as GetProviderCoursesListResponse;
            controllerResult.StatusCode.Should().Be((int)HttpStatusCode.OK);
            model.StandardIds.Count().Should().Be(queryResult.CourseIds.Count());
            model.TotalResults.Should().Be(queryResult.CourseIds.Count());
        }

        [Test, RecursiveMoqAutoData]
        public async Task And_Exception_Then_Returns_Bad_Request(
            int ukprn,
            [Frozen] Mock<IMediator> mockMediator,
            [Greedy] ProvidersController controller)
        {
            mockMediator
                .Setup(mediator => mediator.Send(
                    It.IsAny<GetProviderCoursesQuery>(),
                    It.IsAny<CancellationToken>()))
                .Throws<InvalidOperationException>();

            var controllerResult = await controller.GetStandardIdsByProvider(ukprn) as StatusCodeResult;

            controllerResult.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        }
    }
}