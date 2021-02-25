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
            int id,
            GetProvidersByStandardRequest request,
            ProviderLocation provider,
            ProviderLocation provider2,
            [Frozen] Mock<IMediator> mockMediator,
            [Greedy] CoursesController controller)
        {
            request.Age = Age.AllAges;
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
            
            var queryResult = new GetCourseProvidersQueryResponse
            {
                Providers = new List<ProviderLocation>{provider, provider2}
            }; 
            
            mockMediator
                .Setup(mediator => mediator.Send(
                    It.Is<GetCourseProvidersQuery>(query => 
                        query.StandardId == id &&
                        query.Lat.Equals(request.Lat) &&
                        query.Lon.Equals(request.Lon) &&
                        query.SortOrder.Equals((short)request.SortOrder) && 
                        query.SectorSubjectArea.Equals(request.SectorSubjectArea) &&
                        query.Level.Equals((short)request.Level) &&
                        query.ShortlistUserId.Equals(request.ShortlistUserId)
                        ), 
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(queryResult);

            var controllerResult = await controller.GetProvidersByStandardId(id, request) as ObjectResult;

            var model = controllerResult.Value as GetCourseProvidersListResponse;
            controllerResult.StatusCode.Should().Be((int)HttpStatusCode.OK);
            model.Providers.Count().Should().Be(queryResult.Providers.Count());
            model.Providers.Sum(c => c.AchievementRates.Count).Should().Be(1);
            model.TotalResults.Should().Be(queryResult.Providers.Count());
        }

        [Test, RecursiveMoqAutoData]
        public async Task And_Exception_Then_Returns_Bad_Request(
            int id,
            GetProvidersByStandardRequest request,
            [Frozen] Mock<IMediator> mockMediator,
            [Greedy] CoursesController controller)
        {
            mockMediator
                .Setup(mediator => mediator.Send(
                    It.IsAny<GetCourseProvidersQuery>(),
                    It.IsAny<CancellationToken>()))
                .Throws<InvalidOperationException>();

            var controllerResult = await controller.GetProvidersByStandardId(id, request) as StatusCodeResult;

            controllerResult.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        }
    }
    
}