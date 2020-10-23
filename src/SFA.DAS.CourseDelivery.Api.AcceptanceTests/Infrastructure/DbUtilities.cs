using SFA.DAS.CourseDelivery.Data;
using System;
using System.Collections.Generic;
using System.Text;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.CourseDelivery.Domain.Models;

namespace SFA.DAS.CourseDelivery.Api.AcceptanceTests.Infrastructure
{
    public class DbUtilities
    {
        public static void LoadTestData(CourseDeliveryReadonlyDataContext context)
        {
            context.Providers.Add(new Provider
            {
                Ukprn = 20002451,
                Email = "test@test.com",
                Name = "Test provider",
            });
            context.ProviderRegistrations.Add(new ProviderRegistration
            {
                Ukprn = 20002451,
                StatusId = 1,
                StatusDate = DateTime.UtcNow,
                ProviderTypeId = 1
            });
            context.ProviderStandards.Add(new ProviderStandard
            {
                Ukprn = 20002451,
                StandardId = 10
            });
            context.ProviderStandardLocations.Add(new ProviderStandardLocation
            {
                Ukprn = 20002451,
            });
            context.NationalAchievementRates.Add(new NationalAchievementRate
            {
                Ukprn = 20002451,
                Age = Age.AllAges,
                ApprenticeshipLevel = ApprenticeshipLevel.AllLevels,
                SectorSubjectArea = "Test",
                OverallCohort = 10,
                OverallAchievementRate = 80
            });
            context.NationalAchievementRateOverall.Add(new NationalAchievementRateOverall
            {
                ApprenticeshipLevel = ApprenticeshipLevel.AllLevels,
                Age = Age.AllAges,
                OverallAchievementRate = 90,
                OverallCohort = 100,
                SectorSubjectArea = "Test"
            });
            context.SaveChanges();
        }

        public static void ClearTestData(CourseDeliveryDataContext context)
        {
            
            context.SaveChanges();
        }

    }
}
