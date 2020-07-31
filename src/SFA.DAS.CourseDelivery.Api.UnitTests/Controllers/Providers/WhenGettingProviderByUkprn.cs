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
using SFA.DAS.CourseDelivery.Application.Provider.Queries.Provider;
using SFA.DAS.CourseDelivery.Application.Provider.Queries.ProvidersByCourse;
using SFA.DAS.Testing.AutoFixture;
using GetProviderResponse = SFA.DAS.CourseDelivery.Api.ApiResponses.GetProviderResponse;

namespace SFA.DAS.CourseDelivery.Api.UnitTests.Controllers.Providers
{
    public class WhenGettingProviderByUkprn
    {
        [Test, RecursiveMoqAutoData]
        public async Task Then_Gets_Providers_List_From_Mediator(
            int standardId,
            Application.Provider.Queries.Provider.GetProviderResponse queryResult,
            [Frozen] Mock<IMediator> mockMediator,
            [Greedy] ProvidersController controller)
        {
            mockMediator
                .Setup(mediator => mediator.Send(
                    It.Is<GetProviderQuery>(query => 
                        query.Ukprn == standardId), 
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(queryResult);

            var controllerResult = await controller.GetProviderByUkprn(standardId) as ObjectResult;

            var model = controllerResult.Value as GetProviderResponse;
            controllerResult.StatusCode.Should().Be((int)HttpStatusCode.OK);
            model.Should().BeAssignableTo<GetProviderResponse>();
        }

        [Test, RecursiveMoqAutoData]
        public async Task And_Null_Then_Returns_NotFound_Request(
            int standardId,
            [Frozen] Mock<IMediator> mockMediator,
            [Greedy] ProvidersController controller)
        {
            mockMediator
                .Setup(mediator => mediator.Send(
                    It.IsAny<GetProviderQuery>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Application.Provider.Queries.Provider.GetProviderResponse());

            var controllerResult = await controller.GetProviderByUkprn(standardId) as StatusCodeResult;

            controllerResult.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
        }
    }
}