using SFA.DAS.CourseDelivery.Data;
using System;
using System.Collections.Generic;
using SFA.DAS.CourseDelivery.Domain.Entities;

namespace SFA.DAS.CourseDelivery.Api.AcceptanceTests.Infrastructure
{
    public class DbUtilities
    {
        public static void LoadTestData(CourseDeliveryReadonlyDataContext context)
        {
            context.Providers.AddRange(GetAllProviders());
            context.ProviderRegistrations.AddRange(GetAllProviderRegistrations());
            context.ProviderStandards.AddRange(GetAllProviderStandards());
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
            context.Shortlists.AddRange(GetAllShortlists());
            context.SaveChanges();
        }

        public static void ClearTestData(CourseDeliveryDataContext context)
        {
            context.Providers.RemoveRange(GetAllProviders());
            context.ProviderStandards.RemoveRange(GetAllProviderStandards());
            context.ProviderRegistrations.RemoveRange(GetAllProviderRegistrations());
            context.Shortlists.RemoveRange(GetAllShortlists());
            context.SaveChanges();
        }

        public static IEnumerable<Provider> GetAllProviders()
        {
            return new List<Provider>
            {
                new Provider
                {
                    Ukprn = 20002451,
                    Email = "test@test.com",
                    Name = "Test provider",
                    TradingName = "Test Alternate Trading Name"
                },new Provider
                {
                    Ukprn = 20002452,
                    Email = "test2@test.com",
                    Name = "Test2 provider",
                    TradingName = "Test2 Alternate Trading Name"
                }
            };
        }

        public static IEnumerable<ProviderStandard> GetAllProviderStandards()
        {
            return new List<ProviderStandard>
            {
                new ProviderStandard
                {
                    Ukprn = 20002451,
                    StandardId = 10
                },
                /*new ProviderStandard
                {
                    Ukprn = 20002451,
                    StandardId = 11
                }*/
            };
        }

        public static IEnumerable<ProviderRegistration> GetAllProviderRegistrations()
        {
            return new List<ProviderRegistration>
            {
                new ProviderRegistration
                {
                    Ukprn = 20002451,
                    StatusId = 1,
                    StatusDate = DateTime.UtcNow,
                    ProviderTypeId = 1
                }
            };
        }

        public static string ShortlistUserId = "d6d467c4-28fb-4993-ac97-f1b1f865fd69";

        public static IEnumerable<Shortlist> GetAllShortlists()
        {
            return new List<Shortlist>
            {
                new Shortlist
                {
                    Id = Guid.Parse("40b2a8aa-11f5-4418-a84d-cfcb5f922a32"),
                    ShortlistUserId = Guid.Parse(ShortlistUserId),
                    ProviderUkprn = 20002451,
                    CourseId = 10,
                    LocationDescription = "Somewhere nice",
                    Lat = 0,
                    Long = 0
                },
                new Shortlist
                {
                    Id = Guid.Parse("74f5be32-5e47-4ef2-94e2-a8de66e14148"),
                    ShortlistUserId = Guid.Parse(ShortlistUserId),
                    ProviderUkprn = 20002451,
                    CourseId = 11
                },
                new Shortlist
                {
                    Id = Guid.Parse("0f21cf96-5c4f-4f2b-9c1c-1e2a3bf5b72e"),
                    ShortlistUserId = Guid.Parse("172d5dae-d652-447d-ae7e-a95cb2fcbb72"),//different user
                    ProviderUkprn = 20002451,
                    CourseId = 10,
                    LocationDescription = "Different location"
                }
            };
        }
    }
}
