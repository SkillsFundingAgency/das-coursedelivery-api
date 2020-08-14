using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Application.OverallNationalAchievementRates.Queries;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.CourseDelivery.Domain.Interfaces;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CourseDelivery.Application.UnitTests.OverallNationalAchievementRates.Queries
{
    public class WhenGettingOverallAchievementRatesBySector
    {
        [Test, MoqAutoData]
        public async Task Then_Gets_The_Overall_National_Achievement_Rates_From_The_Service(
            GetOverallAchievementRatesQuery query,
            List<NationalAchievementRateOverall> serviceResponse,
            [Frozen] Mock<IOverallNationalAchievementRateService> service,
            GetOverallAchievementRatesHandler handler
            )
        {
            service.Setup(x => x.GetItemsBySectorSubjectArea(query.SectorSubjectArea))
                .ReturnsAsync(new List<NationalAchievementRateOverall>(serviceResponse));

            var actual = await handler.Handle(query, CancellationToken.None);

            actual.OverallAchievementRates.Should().BeEquivalentTo(serviceResponse);
        }
    }
}