using System;
using System.ComponentModel.DataAnnotations;
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
using SFA.DAS.CourseDelivery.Application.Provider.Queries.GetUkprnsByCourseAndLocation;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CourseDelivery.Api.UnitTests.Controllers.Courses
{
    public class WhenGettingProvidersByCourseAndLocation
    {
        [Test, RecursiveMoqAutoData]
        public async Task Then_Calls_Mediator_To_Get_Ids(
            int standardId,
            double lon,
            double lat,
            GetUkprnsQueryResult result,
            [Frozen] Mock<IMediator> mockMediator,
            [Greedy] CoursesController controller)
        {
            mockMediator.Setup(mediator => mediator.Send(
                It.Is<GetUkprnsQuery>(c =>
                    c.StandardId.Equals(standardId)
                    && c.Lat.Equals(lat)
                    && c.Lon.Equals(lon)
                ), It.IsAny<CancellationToken>())).ReturnsAsync(result);
            
            var controllerResult = await controller.GetProviderIdsByStandardAndLocation(standardId,lat,lon) as ObjectResult;
            
            controllerResult.StatusCode.Should().Be((int)HttpStatusCode.OK);
            var actualModel = controllerResult.Value as GetProvidersByCourseAndLocationResponse;
            actualModel.UkprnsByStandardAndLocation.Should().BeEquivalentTo(result.UkprnsByStandardAndLocation);
            actualModel.UkprnsByStandard.Should().BeEquivalentTo(result.UkprnsByStandard);
        }
        
        [Test, RecursiveMoqAutoData]
        public async Task And_Exception_Then_Returns_Bad_Request(
            int standardId,
            double lon,
            double lat,
            [Frozen] Mock<IMediator> mockMediator,
            [Greedy] CoursesController controller)
        {
            mockMediator
                .Setup(mediator => mediator.Send(
                    It.IsAny<GetUkprnsQuery>(),
                    It.IsAny<CancellationToken>()))
                .Throws<ValidationException>();

            var controllerResult = await controller.GetProviderIdsByStandardAndLocation(standardId,lat,lon) as BadRequestObjectResult;

            controllerResult.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        }
    }
}