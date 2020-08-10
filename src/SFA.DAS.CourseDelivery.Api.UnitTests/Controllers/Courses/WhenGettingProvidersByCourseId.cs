using System;
using System.Collections.Generic;
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
using SFA.DAS.CourseDelivery.Api.ApiRequests;
using SFA.DAS.CourseDelivery.Api.ApiResponses;
using SFA.DAS.CourseDelivery.Api.Controllers;
using SFA.DAS.CourseDelivery.Application.Provider.Queries.ProvidersByCourse;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.Testing.AutoFixture;
using Age = SFA.DAS.CourseDelivery.Api.ApiRequests.Age;

namespace SFA.DAS.CourseDelivery.Api.UnitTests.Controllers.Courses
{
    public class WhenGettingProvidersByCourseId
    {
        [Test, RecursiveMoqAutoData]
        public async Task Then_Gets_Providers_List_From_Mediator_Using_Params(
            int standardId,
            Provider provider,
            Provider provider2,
            [Frozen] Mock<IMediator> mockMediator,
            [Greedy] CoursesController controller)
        {
            provider.NationalAchievementRates.Clear();
            provider.NationalAchievementRates.Add(new NationalAchievementRate
            {
                Age = Domain.Entities.Age.SixteenToEighteen,
                ApprenticeshipLevel = ApprenticeshipLevel.AllLevels
            });
            provider2.NationalAchievementRates.Clear();
            provider2.NationalAchievementRates.Add(new NationalAchievementRate
            {
                Age = Domain.Entities.Age.AllAges,
                ApprenticeshipLevel = ApprenticeshipLevel.AllLevels
            });
            
            var queryResult = new GetCourseProvidersResponse
            {
                Providers = new List<Provider>{provider, provider2}
            }; 
            
            mockMediator
                .Setup(mediator => mediator.Send(
                    It.Is<GetCourseProvidersQuery>(query => 
                        query.StandardId == standardId), 
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(queryResult);

            var controllerResult = await controller.GetProvidersByStandardId(standardId, Age.AllAges, Level.AllLevels) as ObjectResult;

            var model = controllerResult.Value as GetCourseProvidersListResponse;
            controllerResult.StatusCode.Should().Be((int)HttpStatusCode.OK);
            model.Providers.Count().Should().Be(queryResult.Providers.Count());
            model.Providers.Sum(c => c.AchievementRates.Count).Should().Be(1);
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