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
using SFA.DAS.CourseDelivery.Application.OverallNationalAchievementRates.Queries;
using SFA.DAS.CourseDelivery.Application.Provider.Queries.Provider;
using SFA.DAS.Testing.AutoFixture;
using GetOverallAchievementRatesResponse = SFA.DAS.CourseDelivery.Api.ApiResponses.GetOverallAchievementRatesResponse;

namespace SFA.DAS.CourseDelivery.Api.UnitTests.Controllers.AchievementRates
{
    public class WhenGettingOverallAchievementRates
    {
        [Test, RecursiveMoqAutoData]
        public async Task Then_Gets_OverallAchievementRates_List_From_Mediator(
            string sectorSubjectArea,
            Application.OverallNationalAchievementRates.Queries.GetOverallAchievementRatesResponse queryResult,
            [Frozen] Mock<IMediator> mockMediator,
            [Greedy] AchievementRatesController controller)
        {
            mockMediator
                .Setup(mediator => mediator.Send(
                    It.Is<GetOverallAchievementRatesQuery>(query => 
                        query.SectorSubjectArea == sectorSubjectArea), 
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(queryResult);

            var controllerResult = await controller.GetOverallAchievementRates(sectorSubjectArea) as ObjectResult;

            var model = controllerResult.Value as GetOverallAchievementRatesResponse;
            controllerResult.StatusCode.Should().Be((int)HttpStatusCode.OK);
            model.Should().BeAssignableTo<GetOverallAchievementRatesResponse>();
        }

        [Test, RecursiveMoqAutoData]
        public async Task And_Null_Then_Returns_NotFound_Request(
            string sectorSubjectArea,
            [Frozen] Mock<IMediator> mockMediator,
            [Greedy] AchievementRatesController controller)
        {
            mockMediator
                .Setup(mediator => mediator.Send(
                    It.IsAny<GetOverallAchievementRatesQuery>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Application.OverallNationalAchievementRates.Queries.GetOverallAchievementRatesResponse());

            var controllerResult = await controller.GetOverallAchievementRates(sectorSubjectArea) as StatusCodeResult;

            controllerResult.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
        }
    }
}