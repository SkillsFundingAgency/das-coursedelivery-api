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
using SFA.DAS.CourseDelivery.Application.Provider.Queries.ProviderByCourse;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CourseDelivery.Api.UnitTests.Controllers.Courses
{
    public class WhenGettingProviderByUkprnAndStandard
    {
        [Test, RecursiveMoqAutoData]
        public async Task Then_Gets_Providers_List_From_Mediator(
            int standardId,
            int ukPrn,
            string sectorSubjectArea,
            double lat,
            double lon,
            GetCourseProviderQueryResponse queryResult,
            [Frozen] Mock<IMediator> mockMediator,
            [Greedy] CoursesController controller)
        {
            mockMediator
                .Setup(mediator => mediator.Send(
                    It.Is<GetCourseProviderQuery>(query => 
                        query.Ukprn == ukPrn 
                        && query.StandardId == standardId
                        && query.Lat.Equals(lat)
                        && query.Lon.Equals(lon)
                        && query.SectorSubjectArea.Equals(sectorSubjectArea)
                        ), 
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(queryResult);

            var controllerResult = await controller.GetProviderByUkprn(standardId, ukPrn,sectorSubjectArea, lat, lon) as ObjectResult;

            var model = controllerResult.Value as GetProviderDetailResponse;
            controllerResult.StatusCode.Should().Be((int)HttpStatusCode.OK);
            model.Should().BeAssignableTo<GetProviderDetailResponse>();
        }

        [Test, RecursiveMoqAutoData]
        public async Task And_Null_Then_Returns_NotFound_Request(
            int standardId,
            int ukPrn,
            string sectorSubjectArea,
            [Frozen] Mock<IMediator> mockMediator,
            [Greedy] CoursesController controller)
        {
            mockMediator
                .Setup(mediator => mediator.Send(
                    It.IsAny<GetCourseProviderQuery>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(new GetCourseProviderQueryResponse());

            var controllerResult = await controller.GetProviderByUkprn(standardId, ukPrn, sectorSubjectArea) as StatusCodeResult;

            controllerResult.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
        }
    }
}