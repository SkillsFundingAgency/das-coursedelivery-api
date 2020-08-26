using System.Collections.Generic;
using System.Linq;
using SFA.DAS.CourseDelivery.Domain.Entities;

namespace SFA.DAS.CourseDelivery.Domain.Models
{
    public class ProviderLocation
    {
        public ProviderLocation(int ukPrn, string name, IReadOnlyCollection<ProviderWithStandardAndLocation> providerWithStandardAndLocations)
        {
            Ukprn = ukPrn;
            Name = name;
            DeliveryTypes = providerWithStandardAndLocations.GroupBy(x=>new {x.DeliveryModes, x.LocationId,x.DistanceInMiles})
                .Select(p=>p.FirstOrDefault())
                .Select(c => (DeliveryType) c).ToList();
            AchievementRates = providerWithStandardAndLocations
                .Where(c=>c.Age.HasValue && c.ApprenticeshipLevel.HasValue).Select(c => (AchievementRate) c).ToList();
        }

        public ProviderLocation(Provider provider)
        {
            Ukprn = provider.Ukprn;
            Name = provider.Name;
            AchievementRates = provider.NationalAchievementRates.Select(c => (AchievementRate) c).ToList();
            DeliveryTypes = new List<DeliveryType>();
        }

        public int Ukprn { get; }
        public string Name { get; }
        public List<DeliveryType> DeliveryTypes { get; }
        public List<AchievementRate> AchievementRates { get; set; }
    }
}