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
using SFA.DAS.CourseDelivery.Application.Provider.Queries.ProvidersByCourse;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CourseDelivery.Api.UnitTests.Controllers.Courses
{
    public class WhenGettingProvidersByCourseId
    {
        [Test, RecursiveMoqAutoData]
        public async Task Then_Gets_Providers_List_From_Mediator(
            int standardId,
            GetCourseProvidersResponse queryResult,
            [Frozen] Mock<IMediator> mockMediator,
            [Greedy] CoursesController controller)
        {
            mockMediator
                .Setup(mediator => mediator.Send(
                    It.Is<GetCourseProvidersQuery>(query => 
                        query.StandardId == standardId), 
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(queryResult);

            var controllerResult = await controller.GetProvidersByStandardId(standardId) as ObjectResult;

            var model = controllerResult.Value as GetCourseProvidersListResponse;
            controllerResult.StatusCode.Should().Be((int)HttpStatusCode.OK);
            model.Providers.Count().Should().Be(queryResult.Providers.Count());
            model.TotalResults.Should().Be(queryResult.Providers.Count());
        }

        [Test, RecursiveMoqAutoData]
        public async Task And_Exception_Then_Returns_Bad_Request(
            int standardId,
            [Frozen] Mock<IMediator> mockMediator,
            [Greedy] CoursesController controller)
        {
            mockMediator
                .Setup(mediator => mediator.Send(
                    It.IsAny<GetCourseProvidersQuery>(),
                    It.IsAny<CancellationToken>()))
                .Throws<InvalidOperationException>();

            var controllerResult = await controller.GetProvidersByStandardId(standardId) as StatusCodeResult;

            controllerResult.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        }
    }
    
}