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
using SFA.DAS.CourseDelivery.Domain.Models;
using SFA.DAS.Testing.AutoFixture;
using Age = SFA.DAS.CourseDelivery.Api.ApiRequests.Age;

namespace SFA.DAS.CourseDelivery.Api.UnitTests.Controllers.Courses
{
    public class WhenGettingProvidersByCourseId
    {
        [Test, RecursiveMoqAutoData]
        public async Task Then_Gets_Providers_List_From_Mediator_Using_Params(
            int standardId,
            double? lat,
            double? lon,
            ProviderLocation provider,
            ProviderLocation provider2,
            [Frozen] Mock<IMediator> mockMediator,
            [Greedy] CoursesController controller)
        {
            provider.AchievementRates.Clear();
            provider.AchievementRates.Add(new NationalAchievementRate
            {
                Age = Domain.Entities.Age.SixteenToEighteen,
                ApprenticeshipLevel = ApprenticeshipLevel.AllLevels
            });
            provider2.AchievementRates.Clear();
            provider2.AchievementRates.Add(new NationalAchievementRate
            {
                Age = Domain.Entities.Age.AllAges,
                ApprenticeshipLevel = ApprenticeshipLevel.AllLevels
            });
            
            var queryResult = new GetCourseProvidersResponse
            {
                Providers = new List<ProviderLocation>{provider, provider2}
            }; 
            
            mockMediator
                .Setup(mediator => mediator.Send(
                    It.Is<GetCourseProvidersQuery>(query => 
                        query.StandardId == standardId &&
                        query.Lat.Equals(lat) &&
                        query.Lon.Equals(lon)
                        ), 
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(queryResult);

            var controllerResult = await controller.GetProvidersByStandardId(standardId, Age.AllAges, Level.AllLevels, lat, lon) as ObjectResult;

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