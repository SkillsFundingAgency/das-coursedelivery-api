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
using SFA.DAS.Testing.AutoFixture;
using GetProviderResponse = SFA.DAS.CourseDelivery.Application.Provider.Queries.Provider.GetProviderResponse;

namespace SFA.DAS.CourseDelivery.Api.UnitTests.Controllers.Providers
{
    public class WhenGettingProviderByUkprn
    {
        [Test, RecursiveMoqAutoData]
        public async Task Then_Gets_Providers_List_From_Mediator(
            int ukPrn,
            GetProviderResponse queryResult,
            [Frozen] Mock<IMediator> mockMediator,
            [Greedy] ProvidersController controller)
        {
            mockMediator
                .Setup(mediator => mediator.Send(
                    It.Is<GetProviderQuery>(query => 
                        query.Ukprn == ukPrn), 
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(queryResult);

            var controllerResult = await controller.GetProvider(ukPrn) as ObjectResult;

            var model = controllerResult.Value as GetProviderDetailsResponse;
            controllerResult.StatusCode.Should().Be((int)HttpStatusCode.OK);
            model.Should().BeEquivalentTo((GetProviderDetailsResponse)queryResult.Provider);
        }

        [Test, RecursiveMoqAutoData]
        public async Task And_Null_Then_Returns_BadRequest(
            int ukPrn,
            [Frozen] Mock<IMediator> mockMediator,
            [Greedy] ProvidersController controller)
        {
            mockMediator
                .Setup(mediator => mediator.Send(
                    It.IsAny<GetProviderQuery>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(new GetProviderResponse());

            var controllerResult = await controller.GetProvider(ukPrn) as StatusCodeResult;

            controllerResult.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        }
    }
}