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
using SFA.DAS.CourseDelivery.Application.Provider.Queries.Providers;
using SFA.DAS.Testing.AutoFixture;
using GetProvidersResponse = SFA.DAS.CourseDelivery.Application.Provider.Queries.Providers.GetProvidersResponse;

namespace SFA.DAS.CourseDelivery.Api.UnitTests.Controllers.Providers
{
    public class WhenGettingAllProviders
    {
        [Test, MoqAutoData]
        public async Task Then_Gets_Providers_From_Mediator(
            GetProvidersResponse queryResult,
            [Frozen] Mock<IMediator> mockMediator,
            [Greedy] ProvidersController controller)
        {
            mockMediator
                .Setup(mediator => mediator.Send(
                    It.IsAny<GetProvidersQuery>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(queryResult);

            var controllerResult = await controller.GetProviders() as ObjectResult;

            var model = controllerResult.Value as Api.ApiResponses.GetProvidersResponse;
            controllerResult.StatusCode.Should().Be((int)HttpStatusCode.OK);
            model.Providers.Should().BeEquivalentTo(queryResult.RegisteredProviders);
        }

        [Test, MoqAutoData]
        public async Task And_Exception_Then_Returns_Bad_Request(
            [Frozen] Mock<IMediator> mockMediator,
            [Greedy] ProvidersController controller)
        {
            mockMediator
                .Setup(mediator => mediator.Send(
                    It.IsAny<GetProvidersQuery>(),
                    It.IsAny<CancellationToken>()))
                .Throws<InvalidOperationException>();

            var controllerResult = await controller.GetProviders() as StatusCodeResult;

            controllerResult.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        }
    }
}